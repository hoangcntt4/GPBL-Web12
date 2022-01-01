/**
 * Lop phan trang
 * @author nvchung
 */
class Pagination {
  /**
   * @callback onPageChangeCallback
   */
  /**
   * @param {Object} config - cau hinh phan trang
   * @param {string} config.paginationId - #id cua element
   * @param {Object[]} config.data - du lieu phan trang
   * @param {number} config.perPage - so ban ghi tren trang
   * @param {number} config.maxDisplayPage - so link trang toi da duoc hien thi
   * @param {number} config.currentPage - trang hien tai
   * @param {onPageChangeCallback} config.onPageChange - callback duoc goi khi currentPage thay doi
   */
  constructor({
    paginationId,
    data,
    perPage = 5,
    maxDisplayPage = 5,
    currentPage = 1,
    onPageChange,
  }) {
    this.paginationHtml = $(paginationId);
    this.data = data;
    this.perPage = perPage;
    this.currentPage = currentPage;
    this.maxDisplayPage = maxDisplayPage;
    this.totalPages = Math.ceil(data.length / perPage);
    this.onPageChange = onPageChange;
    this.renderPageList();
  }
  /**
   * set data
   * @param {Object[]} data
   */
  setData(data) {
    this.data = data;
    this.totalPages = Math.ceil(data.length / this.perPage);
    this.firstPage();
  }
  /**
   * set perPage
   * @param {number} perPage
   */
  setPerPage(perPage) {
    this.perPage = perPage;
    this.totalPages = Math.ceil(this.data.length / perPage);
    this.firstPage();
  }
  /**
   * tra ve data tren trang hien tai
   * @returns {Object[]}
   */
  getCurrentPageData() {
    const start = (this.currentPage - 1) * this.perPage;
    return this.data.slice(start, start + this.perPage);
  }
  /**
   * Ham render html
   */
  renderPageList() {
    this.paginationHtml.empty();
    this.paginationHtml.addClass("c-page-list");
    const firstPage = $(`<li class="c-page-item">
		<img src="/asset/icon/btn-firstpage.svg" alt="icon" />
		</li>`).on("click", () => this.firstPage());
    const prevPage = $(`<li class="c-page-item">
		<img src="/asset/icon/btn-prev-page.svg" alt="" />
		</li>`).on("click", () => this.prevPage());
    const nextPage = $(`<li class="c-page-item">
		<img src="/asset/icon/btn-next-page.svg" alt="icon" />
		</li>`).on("click", () => this.nextPage());
    const lastPage = $(`<li class="c-page-item">
		<img src="/asset/icon/btn-lastpage.svg" alt="icon" />
		</li>`).on("click", () => this.lastPage());
    let startPage = Math.max(
      1,
      this.currentPage - Math.floor(this.maxDisplayPage / 2)
    );
    const endPage = Math.min(
      this.totalPages,
      startPage + this.maxDisplayPage - 1
    );
    if (endPage == this.totalPages && endPage - this.maxDisplayPage > -1) {
      startPage = endPage - this.maxDisplayPage + 1;
    }
    for (let i = startPage; i <= endPage; i++) {
      const page = $(`<li class="c-page-item"></li>`)
        .text(i)
        .on("click", () => this.page(i));
      if (this.currentPage == i) page.addClass("active");
      this.paginationHtml.append(page);
    }
    if (this.currentPage == 1) {
      firstPage.addClass("disabled");
      prevPage.addClass("disabled");
    }
    if (this.currentPage == this.totalPages) {
      lastPage.addClass("disabled");
      nextPage.addClass("disabled");
    }
    this.paginationHtml.prepend(firstPage, prevPage);
    this.paginationHtml.append(nextPage, lastPage);
  }
  /**
   * nhay den trang dau tien
   */
  firstPage() {
    this.currentPage = 1;
    this.renderPageList();
    this.onPageChange();
  }
  /**
   * nhay den trang cuoi cung
   */
  lastPage() {
    this.currentPage = this.totalPages;
    this.renderPageList();
    this.onPageChange();
  }
  /**
   * nhay den trang ke tiep
   */
  nextPage() {
    this.currentPage = Math.min(this.totalPages, this.currentPage + 1);
    this.renderPageList();
    this.onPageChange();
  }
  /**
   * nhay den trang truoc
   */
  prevPage() {
    this.currentPage = Math.max(1, this.currentPage - 1);
    this.renderPageList();
    this.onPageChange();
  }
  /**
   * nhay den trang bat ky
   * @param {number} num - so trang
   */
  page(num) {
    this.currentPage = num;
    this.renderPageList();
    this.onPageChange();
  }
}
