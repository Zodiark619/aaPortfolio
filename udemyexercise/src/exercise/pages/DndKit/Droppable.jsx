import { useDroppable } from "@dnd-kit/react";

export default function Droppable({ id, title, children, count }) {
  const { ref, isDropTarget } = useDroppable({ id });

  return (
    <div
      ref={ref}
      style={{
        width: 300,
        minHeight: 480,
        backgroundColor: isDropTarget ? "#e3f2fd" : "#f4f5f7",
        borderRadius: 12,
        border: isDropTarget ? "2px solid #2196f3" : "2px solid transparent",
        display: "flex",
        flexDirection: "column",
        transition: "background-color 0.15s, border 0.15s",
      }}
    >
      {/* Header */}
      <div
        style={{
          padding: "14px 16px 10px",
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <h3
          style={{
            margin: 0,
            fontSize: 13,
            fontWeight: 600,
            color: "#172b4d",
            textTransform: "uppercase",
            letterSpacing: "0.4px",
          }}
        >
          {title}
        </h3>
        <span
          style={{
            background: "#dfe1e6",
            color: "#5e6c84",
            fontSize: 12,
            fontWeight: 600,
            padding: "2px 8px",
            borderRadius: 10,
          }}
        >
          {count}
        </span>
      </div>

      {/* Cards / Empty state */}
      <div
        style={{
          padding: "4px 12px 16px",
          display: "flex",
          flexDirection: "column",
          gap: 10,
          flex: 1,
        }}
      >
        {count === 0 ? (
          <div
            style={{
              flex: 1,
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              color: "#9e9e9e",
              fontSize: 13,
              border: "2px dashed #d0d0d0",
              borderRadius: 8,
              minHeight: 80,
            }}
          >
            Drop tasks here
          </div>
        ) : (
          children
        )}
      </div>
    </div>
  );
}
