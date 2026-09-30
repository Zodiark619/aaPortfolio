import { useState, useEffect } from "react";
import { DragDropProvider, DragOverlay } from "@dnd-kit/react";
import { move } from "@dnd-kit/helpers";
import Droppable from "./Droppable";
import SortableItem from "./SortableItem";

const STORAGE_KEY = "kanban-tasks";

const COLUMNS = [
  { id: "todo", title: "To Do" },
  { id: "in-progress", title: "In Progress" },
  { id: "done", title: "Done" },
];

// Helper: convert flat array ↔ grouped object
function toGrouped(tasks) {
  const grouped = { todo: [], "in-progress": [], done: [] };
  tasks.forEach((task) => {
    if (grouped[task.status]) {
      grouped[task.status].push(task);
    }
  });
  // sort by order inside each column
  Object.keys(grouped).forEach((key) => {
    grouped[key].sort((a, b) => a.order - b.order);
  });
  return grouped;
}

function toFlat(grouped) {
  const result = [];
  Object.entries(grouped).forEach(([status, items]) => {
    items.forEach((task, index) => {
      result.push({ ...task, status, order: index });
    });
  });
  return result;
}

export default function KanbanBoard() {
  const [tasks, setTasks] = useState(() => {
    const saved = localStorage.getItem(STORAGE_KEY);
    return saved
      ? JSON.parse(saved)
      : [
          { id: "1", title: "Learn dnd-kit", status: "todo", order: 0 },
          {
            id: "2",
            title: "Build Kanban UI",
            status: "in-progress",
            order: 0,
          },
          { id: "3", title: "Add localStorage", status: "done", order: 0 },
        ];
  });

  const [newTitle, setNewTitle] = useState("");

  // Grouped version for sortable
  const grouped = toGrouped(tasks);

  useEffect(() => {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(tasks));
  }, [tasks]);

  const addTask = () => {
    if (!newTitle.trim()) return;

    const newTask = {
      id: crypto.randomUUID(),
      title: newTitle.trim(),
      status: "todo",
      order: grouped.todo.length,
      createdAt: Date.now(),
    };

    setTasks((prev) => [...prev, newTask]);
    setNewTitle("");
  };
  const editTask = (id, newTitle) => {
    setTasks((prev) =>
      prev.map((task) =>
        task.id === id ? { ...task, title: newTitle } : task,
      ),
    );
  };
  const deleteTask = (id) => {
    setTasks((prev) => prev.filter((task) => task.id !== id));
  };
  const clearAllTasks = () => {
    if (window.confirm("Are you sure you want to delete all tasks?")) {
      setTasks([]);
    }
  };
  return (
    <div
      style={{
        padding: 24,
        fontFamily: "sans-serif",
        background: "#fafbfc",
        minHeight: "100vh",
      }}
    >
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "flex-start",
          marginBottom: 8,
        }}
      >
        <div>
          <h1 style={{ margin: "0 0 4px", color: "#172b4d" }}>Kanban Board</h1>
          <p style={{ margin: 0, color: "#5e6c84", fontSize: 14 }}>
            {tasks.length} task{tasks.length !== 1 ? "s" : ""} • Drag to reorder
          </p>
        </div>

        {tasks.length > 0 && (
          <button
            onClick={clearAllTasks}
            style={{
              padding: "8px 14px",
              background: "transparent",
              color: "#c62828",
              border: "1px solid #ef9a9a",
              borderRadius: 8,
              fontSize: 13,
              cursor: "pointer",
            }}
          >
            Clear All
          </button>
        )}
      </div>

      {/* Add Task */}
      <div style={{ marginBottom: 28, display: "flex", gap: 8 }}>
        <input
          value={newTitle}
          onChange={(e) => setNewTitle(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && addTask()}
          placeholder="What needs to be done?"
          style={{
            padding: "10px 14px",
            width: 280,
            border: "1px solid #dfe1e6",
            borderRadius: 8,
            fontSize: 14,
            outline: "none",
          }}
        />
        <button
          onClick={addTask}
          style={{
            padding: "10px 18px",
            background: "#0052cc",
            color: "white",
            border: "none",
            borderRadius: 8,
            fontWeight: 600,
            cursor: "pointer",
          }}
        >
          Add Task
        </button>
      </div>

      <DragDropProvider
        onDragOver={(event) => {
          setTasks((prev) => {
            const groupedPrev = toGrouped(prev);
            const newGrouped = move(groupedPrev, event);
            return toFlat(newGrouped);
          });
        }}
        onDragEnd={(event) => {
          if (event.canceled) return;
        }}
      >
        {/* Your columns */}
        <div style={{ display: "flex", gap: 16, alignItems: "flex-start" }}>
          {COLUMNS.map((column) => (
            <Droppable
              key={column.id}
              id={column.id}
              title={column.title}
              count={grouped[column.id].length}
            >
              {grouped[column.id].map((task, index) => (
                <SortableItem
                  key={task.id}
                  id={task.id}
                  index={index}
                  group={column.id}
                  title={task.title}
                  onDelete={() => deleteTask(task.id)}
                  onEdit={editTask}
                />
              ))}
            </Droppable>
          ))}
        </div>

        {/* ========== DRAG OVERLAY ========== */}
        <DragOverlay>
          {(source) => {
            const task = tasks.find((t) => t.id === source.id);
            if (!task) return null;

            return (
              <div
                style={{
                  padding: "12px 14px",
                  background: "white",
                  border: "1px solid #2196f3",
                  borderRadius: 8,
                  boxShadow: "0 12px 28px rgba(0,0,0,0.18)",
                  fontSize: 14,
                  lineHeight: 1.4,
                  color: "#172b4d",
                  width: 260,
                  cursor: "grabbing",
                }}
              >
                {task.title}
              </div>
            );
          }}
        </DragOverlay>
      </DragDropProvider>
    </div>
  );
}
