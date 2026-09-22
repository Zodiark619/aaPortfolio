const LinkComponent = (props) => {
  return (
    <>
      <a href={props.link} target="_blank">
        {props.title}
      </a>
    </>
  );
};

export default LinkComponent;
