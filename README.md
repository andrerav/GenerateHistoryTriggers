# GenerateHistoryTriggers
A simple tool that will generate SQL for history tables and associated triggers for PostgreSQL based on a simple JSON definition file.

## How to use
Enter the project directory
```bash
cd src/GenerateHistoryTriggers
```

Run the project and specify the input definition file with the `-i` argument:

```bash
dotnet run -i .\Input\Blog.example.json
```

To store the output to a file, use an IO redirect:

```bash
dotnet run -i .\Input\Blog.example.json > HistoryTable.sql
```

## How the code generation works

The C# program and T4 template use a JSON definition file to generate the history table scripts, triggers, and functions. This file defines the metadata for each table you want to track changes for.

## JSON Structure

The JSON file is an array of objects, where each object represents the configuration for a table. Below is the structure of each table configuration object:

| Property Name                  | Type     | Description                                                                                                                                       | Required |
|--------------------------------|----------|---------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| `TableName`                    | `string` | The name of the table for which history tracking is being configured.                                                                             | Yes      |
| `SchemaName`                   | `string` | The schema where the main table resides.                                                                                                          | Yes      |
| `HistorySchemaName`            | `string` | The schema where the corresponding history table will be created. If the schema does not exist, it will be created.                                                                                | Yes      |
| `PreviousValidToColumnName`    | `string` | The column or expression from the main table to determine the `active_from` timestamp in the history table if it’s the first entry for a row. You can use `NOW()` if there is no such column, but I strongly urge you to add such a column to the table to have a proper audit record of the data.     | Yes      |
| `PrimaryKeys`                  | `array`  | An array of objects defining the primary key columns of the table.                                                                                | Yes      |

### `PrimaryKeys` Array

Each element in the `PrimaryKeys` array describes a primary key column for the table. The following fields are included:

| Property Name    | Type     | Description                                      | Required |
|-------------------|----------|--------------------------------------------------|----------|
| `PrimaryKeyName` | `string` | The name of the primary key column.              | Yes      |
| `SqlType`        | `string` | The SQL data type of the primary key column.     | Yes      |

## Example JSON

Below is an example JSON definition file for tracking changes to `posts` and `comments` tables:

```json
[
  {
    "TableName": "posts",
    "SchemaName": "public",
    "HistorySchemaName": "history_tables",
    "PreviousValidToColumnName": "OLD.created_date",
    "PrimaryKeys": [
      {
        "PrimaryKeyName": "id",
        "SqlType": "int"
      }
    ]
  },
  {
    "TableName": "comments",
    "SchemaName": "public",
    "HistorySchemaName": "history_tables",
    "PreviousValidToColumnName": "OLD.created_date",
    "PrimaryKeys": [
      {
        "PrimaryKeyName": "id",
        "SqlType": "int"
      }
    ]
  }
]

```


## How the history table logic works

> [!NOTE]
> This explanation is based on the example definition in the included Blog.example.json definition file. 

The `posts_history` table records historical changes made to the `posts` table, capturing the state of a row before updates or deletions occur. This is accomplished using a combination of a trigger function and a trigger, ensuring that changes are logged automatically.

1. **Trigger Function (`fn_posts_update_history`)**:
   - This function is invoked whenever an `UPDATE` or `DELETE` operation is performed on the `posts` table.
   - The function calculates the MD5 hash of the current and previous row data to determine if a change has occurred.
   - If the row data has changed, the function inserts the previous state of the row into the `posts_history` table.
   - The function also manages the `revision` numbers and the `active_from`/`active_to` timestamps to maintain a complete history.

2. **Trigger (`trg_posts_on_update`)**:
   - This trigger is set on the `posts` table.
   - It invokes the `fn_posts_update_history` function after any `UPDATE` or `DELETE` operation.

### Table Definition: `history_tables.posts_history`

The `posts_history` table is structured to store the historical states of rows from the `posts` table. Below is a breakdown of its columns:

| Column Name      | Data Type               | Purpose                                                                                          |
|-------------------|-------------------------|--------------------------------------------------------------------------------------------------|
| `id`             | `int`                  | The primary key of the row in the `posts` table that this history entry corresponds to.         |
| `row_data`       | `jsonb`                | A JSONB representation of the row's state at the time of the change.                            |
| `md5_hash`       | `varchar(32)`          | An MD5 hash of the row's state, used to detect changes in content.                              |
| `revision`       | `integer`              | The revision number of the row, incremented for each historical entry.                         |
| `active_from`    | `timestamp with time zone` | The timestamp indicating when this historical state became active.                             |
| `active_to`      | `timestamp with time zone` | The timestamp indicating when this historical state was replaced by a newer revision (if any). |

### Key Features

- **Automatic Revision Tracking**: The `revision` column increments automatically to track the number of changes made to a row.
- **Time-Based History**: The `active_from` and `active_to` columns define the time range during which a particular historical state was active.
- **Change Detection via Hashing**: MD5 hashes of the row data are used to detect changes, ensuring no duplicate entries are created for identical updates.
- **Comprehensive Auditing**: Both `UPDATE` and `DELETE` operations are logged, enabling complete historical tracking.

### Trigger and Function Logic

#### Trigger Function: `fn_posts_update_history`

- Determines the current revision number for the row in the `posts_history` table.
- Checks if the content of the row has changed by comparing MD5 hashes.
- If the row has changed, inserts the previous state into the `posts_history` table with:
  - The current `revision`.
  - The appropriate `active_from` and `active_to` timestamps.
- Returns the updated or deleted row for further processing.

#### Trigger: `trg_posts_on_update`

- Executes the `fn_posts_update_history` function after an `UPDATE` or `DELETE` operation on the `posts` table.
- Ensures historical tracking is applied consistently to all changes.

## Example Usage

1. **Updating a Post**:
   - When a post is updated, the trigger captures the previous state and inserts it into the `posts_history` table.
   - A new revision is created for the current state.

2. **Deleting a Post**:
   - When a post is deleted, its last known state is recorded in the `posts_history` table with an appropriate `active_to` timestamp.
