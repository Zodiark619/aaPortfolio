import { useState } from "react";

const Dropdown = ({ options, handleSelect, selection }) => {
  const [isOpen, setIsOpen] = useState(false);
  const handleClick = () => {
    setIsOpen(!isOpen);
  };
  const handleOptionClick = (option) => {
    setIsOpen(false);
    handleSelect(option);
  };
  const rendered = options.map((option) => {
    return (
      <div
        key={option.value}
        onClick={() => handleOptionClick(option)}
        className="hover:bg-sky-100 rounded cursor-pointer p-1"
      >
        {option.label}
      </div>
    );
  });

  return (
    <>
      <div className="w-48 relative">
        <div
          onClick={handleClick}
          className="flex justify-between items-center cursor-pointer border rounded p-3 shadow bg-white w-full"
        >
          {selection?.label || <p>selete......</p>}
        </div>
        {isOpen && (
          <div className="absolute top-full border rounded p-3 shadow bg-white w-full">
            {rendered}
          </div>
        )}
      </div>
    </>
  );
};

export default Dropdown;
