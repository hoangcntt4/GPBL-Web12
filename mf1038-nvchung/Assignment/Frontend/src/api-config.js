const BASE = "http://localhost:8080/api/v1";
const EMPLOYEE = `${BASE}/Employees`;
const DEPARTMENT = `${BASE}/Departments`;
/**
 * @readonly
 *@enum {string}
 */
const Apis = {
  Employee: {
    BASE: EMPLOYEE,
    NEXT_CODE: `${EMPLOYEE}/NextCode`,
    FILTER: `${EMPLOYEE}/Filter`,
    BULK: `${EMPLOYEE}/BulkDelete`,
    EXCEL_FILE: `${EMPLOYEE}/ExcelFile`,
  },
  Department: {
    BASE: DEPARTMENT,
  },
};
export default Apis;
