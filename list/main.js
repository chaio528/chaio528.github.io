const title = document.querySelector(".date-title");
const lastMonthBtn = document.querySelector(".last-month-btn");
const nextMonthBtn = document.querySelector(".next-month-btn");
const todayBtn = document.querySelector(".today-btn");
const dateInput = document.querySelector('#date_input');
const todoInput = document.querySelector('#todo_item__input');

const createModal1 = new bootstrap.Modal(document.querySelector('#create_todo_modal'));
const createModal2 = bootstrap.Modal.getOrCreateInstance('#create_todo_modal');

const today = new Date();
let currentYear;
let currentMonth; //從1開始 1~12
 
const localStorageKey = "my-todo";
let todoItemObj = {};
 
todayBtn.addEventListener("click", () => {
  initCalendar();
});
 
lastMonthBtn.addEventListener("click", () => {
  currentMonth--;
  if (currentMonth < 1) {
    currentYear--;
    currentMonth = 12;
  }
  showTitle(currentYear, currentMonth);
  renderingCalendar();
});
 
nextMonthBtn.addEventListener("click", () => {
  currentMonth++;
  if (currentMonth > 12) {
    currentYear++;
    currentMonth = 1;
  }
  showTitle(currentYear, currentMonth);
  renderingCalendar();
});
 
function renderingCalendar() {
  const firstDateOfCurrentMonth = new Date(currentYear, currentMonth - 1, 1);
  const lastDateOfCurrentMonth = new Date(currentYear, currentMonth - 1 + 1, 0);
  /** 日曆的第一格與當月1號的關係（1號星期幾，0-6）：
   * 0,1,2,3,4,5,6
   * 日 一 二 三 四 五 六
   * 1,2,3,4,5,6,7      --->(1-0)
   * 0,1,2,3,4,5,6      --->(1-1)
   * -1,0,1,2,3,4,5     --->(1-2)
   * -2,-1,0,1,2,3,4    --->(1-3)
   * -3,-2,-1,0,1,2,3   --->(1-4)
   * -4,-3,-2,-1,0,1,2  --->(1-5)
   * -5,-4,-3,-2,-1,0,1 --->(1-6)
   */
 
  /** 日曆上顯示的最後一格與當月最後一天的關係(假如 f:30號)
   * 0,1,2,3,4,5,6
   * 日 一 二 三 四 五 六
   * f,1,2,3,4,5,6    --->(30 + (6 - 0))
   *  ,f,1,2,3,4,5    --->(30 + (6 - 1))
   *  , ,f,1,2,3,4    --->(30 + (6 - 2))
   * ......
   *  , , , , ,f,1    --->(30 + (6 - 5))
   *  , , , , , ,f    --->(30 + (6 - 6))
   * .......
   */
 
  let start = 1 - firstDateOfCurrentMonth.getDay();
  const end =
    lastDateOfCurrentMonth.getDate() + (6 - lastDateOfCurrentMonth.getDay());
  const dateArea = document.querySelector(".date-area");
  dateArea.innerHTML = "";
 
  for (start; start <= end; start++) {
    const curr = new Date(currentYear, currentMonth - 1, start);
    //<div class="border col"><span class="d-inline-block text-center w-100"></span></div>
    const dateDom = document.createElement("div");
    dateDom.classList.add("border", "col");
 
    const dateEl = document.createElement("span");
    dateEl.classList.add("d-inline-block", "w-100", "text-center");
    dateEl.textContent = curr.getDate();
    if (
      curr.getFullYear() === today.getFullYear() &&
      curr.getMonth() === today.getMonth() &&
      curr.getDate() === today.getDate()
    ) {
      dateEl.classList.add("badge", "rounded-pill", "text-bg-primary");
    }
    dateDom.append(dateEl);
 
    //TODO: 待辦事項click
    const dateStr = getDateStr(curr);
    
    const currDateStr = dateStr;
    const currTodoItems = todoItemObj[currDateStr];
    if (currTodoItems) {
      const ul = document.createElement("ul");
      currTodoItems.forEach((todo, idx) => {
        const li = document.createElement("li");
        li.textContent = todo;
        ul.append(li);
      });
      dateDom.append(ul);
    }
 
    dateArea.append(dateDom);
  }
}
 
function getDateStr(date) {
  // return '2024-01-09'
  return `${date.getFullYear()}-${(date.getMonth() + 1)
    .toString()
    .padStart(2, "0")}-${date.getDate().toString().padStart(2, "0")}`;
}
 // 初始日曆
function initCalendar() {
  currentYear = today.getFullYear();
  currentMonth = today.getMonth() + 1;
  showTitle(currentYear, currentMonth);
  getTodoFromStorage();
  renderingCalendar();
  dateInput.value = getDateStr(today);
}

 
function showTitle(year, month) {
  title.textContent = `${year} / ${month.toString().padStart(2, 0)}`;
}
 
function setTodoToStorage(dateStr, content) {
  if (!Array.isArray(todoItemObj[dateStr])) {
    todoItemObj[dateStr] = [];
  }
 
  todoItemObj[dateStr].push(content);
  // const obj = {
  //   "2024-01-01": ["todo1", "todo2"],
  //   "2023-12-31": ["跨年", "熬夜"],
  // };
  // let myObjKey = "2024-01-01"
  // obj[myObjKey]
  let jsonStr = JSON.stringify(todoItemObj);
  localStorage.setItem(localStorageKey, jsonStr);
}
 
function getTodoFromStorage() {
  const todoObj = JSON.parse(localStorage.getItem(localStorageKey));
  if (todoObj) {
    todoItemObj = todoObj;
  }
}
 
function createTodo(){
    // 抓日期 ,抓事項
    const dateString = dateInput.value;
    const todoInput = todoInput.value.trim();
    if(todoContent === ''){
        return;
    }
    // 存進去
    setTodoToStorage(dateString ,todoContent);
    todoInput.value ='';
    createModal2.hide();
    renderingCalendar();
}

initCalendar();