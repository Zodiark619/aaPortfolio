import Accordion from "../components/Accordion";

const AccordionPage = () => {
  const items = [
    { id: "213", label: "label1", content: "content1" },
    { id: "bdf", label: "label2", content: "content2" },
    { id: "1232", label: "label3", content: "content3" },
  ];
  return (
    <>
      <div>
        <Accordion items={items} />
        <hr></hr>
        {/*   <KanbanBoard /> */}
      </div>
    </>
  );
};

export default AccordionPage;
