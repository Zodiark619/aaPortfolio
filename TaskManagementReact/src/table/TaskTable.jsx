import { tableFeatures, useTable } from "@tanstack/react-table";
import { useNavigate } from "react-router";
const features = tableFeatures({});
// 4. Define your columns
export default function TaskTable({ data }) {
  const navigate = useNavigate();
  const columns = [
    {
      accessorKey: "name", // accessorKey shorthand
      header: "Task Name",
      cell: (info) => info.getValue(),
    },
    {
      accessorKey: "category", // accessorKey shorthand
      header: "Task Category",
      cell: (info) => info.getValue(),
    },
    {
      accessorKey: "taskStatus", // accessorKey shorthand
      header: "Task Status",
      cell: (info) => info.getValue(),
    },
    {
      accessorKey: "createdByName", // accessorKey shorthand
      header: "Created By Name",

      cell: (info) => info.getValue(),
    },
    {
      accessorKey: "dueDate", // accessorKey shorthand
      header: "Due Date",
      cell: (info) => {
        const value = info.getValue();

        if (!value) {
          return "-";
        }

        return new Date(value).toLocaleDateString();
      },
    },
    {
      id: "actions",
      header: "Actions",
      cell: ({ row }) => {
        const task = row.original;

        return (
          <button
            className="btn btn-sm btn-primary"
            onClick={() => navigate(`/task/${task.id}`)}
          >
            Edit
          </button>
        );
      },
    },
  ];
  const table = useTable({
    features,
    columns,
    data,
  });

  // 6. Render markup from the table instance APIs
  return (
    <table className="table">
      <thead>
        {table.getHeaderGroups().map((headerGroup) => (
          <tr key={headerGroup.id}>
            {headerGroup.headers.map((header) => (
              <th key={header.id}>
                {header.isPlaceholder ? null : (
                  <table.FlexRender header={header} />
                )}
              </th>
            ))}
          </tr>
        ))}
      </thead>
      <tbody>
        {table.getRowModel().rows.map((row) => (
          <tr key={row.id}>
            {row.getAllCells().map((cell) => (
              <td key={cell.id}>
                <table.FlexRender cell={cell} />
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
}
