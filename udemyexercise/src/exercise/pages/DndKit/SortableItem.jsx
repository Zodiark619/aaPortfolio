import { useState } from "react";
import { useSortable } from "@dnd-kit/react/sortable";

export default function SortableItem({
  id,
  index,
  group,
  title,
  onDelete,
  onEdit,
}) {
  const { ref, isDragging } = useSortable({
    id,
    index,
    group,
    type: "item",
    accept: "item",
  });

  const [isEditing, setIsEditing] = useState(false);
  const [editValue, setEditValue] = useState(title);

  const saveEdit = () => {
    const trimmed = editValue.trim();
    if (trimmed && trimmed !== title) {
      onEdit(id, trimmed);
    }
    setIsEditing(false);
  };

  return (
    <div
      ref={ref}
      style={{
        padding: "12px 14px",
        background: isDragging ? "#f0f7ff" : "white",
        border: "1px solid #e0e0e0",
        borderRadius: 8,
        cursor: isEditing ? "text" : "grab",
        opacity: isDragging ? 0.6 : 1,
        boxShadow: isDragging
          ? "0 8px 16px rgba(0,0,0,0.12)"
          : "0 1px 3px rgba(0,0,0,0.08)",
        display: "flex",
        justifyContent: "space-between",
        alignItems: "flex-start",
        gap: 10,
        transition: "box-shadow 0.15s, background 0.15s",
        userSelect: "none",
      }}
    >
      {isEditing ? (
        <input
          autoFocus
          value={editValue}
          onChange={(e) => setEditValue(e.target.value)}
          onBlur={saveEdit}
          onKeyDown={(e) => {
            if (e.key === "Enter") saveEdit();
            if (e.key === "Escape") {
              setEditValue(title);
              setIsEditing(false);
            }
          }}
          style={{
            flex: 1,
            padding: "6px 8px",
            border: "1px solid #2196f3",
            borderRadius: 6,
            fontSize: 14,
            outline: "none",
          }}
        />
      ) : (
        <span
          onDoubleClick={() => {
            setEditValue(title);
            setIsEditing(true);
          }}
          style={{
            flex: 1,
            fontSize: 14,
            lineHeight: 1.4,
            color: "#172b4d",
            cursor: "text",
          }}
          title="Double-click to edit"
        >
          {title}
        </span>
      )}

      <button
        onClick={(e) => {
          e.stopPropagation();
          onDelete();
        }}
        style={{
          background: "transparent",
          border: "none",
          color: "#999",
          cursor: "pointer",
          fontSize: 18,
          lineHeight: 1,
          padding: "0 2px",
          borderRadius: 4,
          flexShrink: 0,
        }}
        onMouseEnter={(e) => (e.currentTarget.style.color = "#e53935")}
        onMouseLeave={(e) => (e.currentTarget.style.color = "#999")}
        title="Delete task"
      >
        ×
      </button>
    </div>
  );
}
