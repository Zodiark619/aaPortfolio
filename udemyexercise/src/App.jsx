import { useState } from "react";

const App = () => {
  const stars = Array.from({ length: 5 }, (_, i) => i + 1);
  const [rating, setRating] = useState(0);
  const [hover, setHover] = useState(0);
  return (
    <>
      <div className="rating-container">
        <h1 className="">Rate your experience</h1>
        <div className="stars">
          {stars.map((star) => (
            <span
              onClick={() => setRating(star)}
              onMouseEnter={() => setHover(star)}
              onMouseLeave={() => setHover(0)}
              className={`star ${star <= (hover || rating) ? "active" : ""}`}
              key={star}
            >
              {"\u2605"}
            </span>
          ))}
        </div>
      </div>
    </>
  );
};

export default App;
