import { useState } from "react";
import { FaChevronCircleDown } from "react-icons/fa";
import { FaChevronCircleLeft } from "react-icons/fa";
const Accordion = ({ items }) => {
  const [expandedIndex, setExpandedIndex] = useState(-1);
  const rendered = items.map((item, index) => {
    const isExpanded = expandedIndex == index;
    const icon = (
      <span className="text-2xl">
        {isExpanded ? <FaChevronCircleDown /> : <FaChevronCircleLeft />}
      </span>
    );
    return (
      <div key={item.id} className="">
        <div
          onClick={() => {
            setExpandedIndex((prev) => {
              if (index === expandedIndex) {
                return -1;
              } else {
                return index;
              }
            });
          }}
          className="flex justify-between p-3 bg-gray-50 border-b items-center cursor-pointer"
        >
          {item.label}
          {icon}
        </div>
        {isExpanded && <div className="border-b p-5">{item.content}</div>}
      </div>
    );
  });
  return (
    <>
      <div className="border-x border-t rounded">{rendered}</div>
    </>
  );
};

export default Accordion;
