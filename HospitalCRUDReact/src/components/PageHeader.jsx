const PageHeader = ({ title, handleCreate }) => {
  return (
    <>
      {/* 1. Page Header   */}
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h1 className="h3 mb-0">{title}</h1>
        <button
          type="button"
          href="create.html"
          className="btn btn-primary"
          onClick={handleCreate}
        >
          + Add New
        </button>
      </div>
    </>
  );
};

export default PageHeader;
