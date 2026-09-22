import LinkComponent from "./LinkComponent";

const CapstoneProject1Contact = () => {
  return (
    <>
      <div className="container">
        <div className="row">
          <h1>Contacts</h1>
          <ul className="list-group">
            <li className="list-group-item">
              Email = herrywijaya.33@gmail.com
            </li>
            <li className="list-group-item">
              <LinkComponent
                title={"Linkedin = linkedin.com/in/herry-wijaya-78b7a5216/"}
                link={"https://www.linkedin.com/in/herry-wijaya-78b7a5216/"}
              />
            </li>
            <li className="list-group-item">
              <LinkComponent
                title={"Github = github.com/Zodiark619"}
                link={"https://www.github.com/Zodiark619"}
              />
            </li>
            <li className="list-group-item">
              <LinkComponent
                title={
                  "Website = https://zodiark619.github.io/mainherrywijaya2026"
                }
                link={"https://zodiark619.github.io/mainherrywijaya2026"}
              />
            </li>
          </ul>
        </div>
      </div>
    </>
  );
};

export default CapstoneProject1Contact;
