const Pagination = ({ pagination, loadData }) => {
  return (
    <>
      {pagination.totalCount > 0 && (
        <nav aria-label="Page navigation">
          <ul className="pagination">
            {/* Previous */}
            <li
              className={`page-item ${pagination.page === 1 ? "disabled" : ""}`}
            >
              <button
                className="page-link"
                onClick={() => loadData(pagination.page - 1)}
                disabled={pagination.page === 1}
              >
                «
              </button>
            </li>

            {/* Page numbers */}
            {Array.from(
              { length: pagination.totalPages },
              (_, index) => index + 1,
            ).map((pageNumber) => (
              <li
                key={pageNumber}
                className={`page-item ${pageNumber === pagination.page ? "active" : ""}`}
              >
                <button
                  className="page-link"
                  onClick={() => loadData(pageNumber)}
                >
                  {pageNumber}
                </button>
              </li>
            ))}

            {/* Next */}
            <li
              className={`page-item ${
                pagination.page === pagination.totalPages ? "disabled" : ""
              }`}
            >
              <button
                className="page-link"
                onClick={() => loadData(pagination.page + 1)}
                disabled={pagination.page === pagination.totalPages}
              >
                »
              </button>
            </li>
          </ul>
        </nav>
      )}
    </>
  );
};

export default Pagination;
