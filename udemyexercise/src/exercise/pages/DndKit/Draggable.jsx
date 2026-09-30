import { useDraggable } from "@dnd-kit/react";

export default function Draggable({ id, children }) {
  const { ref, isDragging } = useDraggable({ id });

  return (
    <div
      ref={ref}
      style={{
        padding: "10px 12px",
        background: "white",
        border: "1px solid #ddd",
        borderRadius: 6,
        cursor: "grab",
        opacity: isDragging ? 0.5 : 1,
        boxShadow: "0 1px 3px rgba(0,0,0,0.1)",
      }}
    >
      {children}
    </div>
  );
}
