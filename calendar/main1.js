const title = document.querySelector(".date-title");
const lastMonthBtn = document.querySelector(".last-month-btn");
const nextMonthBtn = document.querySelector(".next-month-btn");
const todayBtn = document.querySelector(".today-btn");
const dateInput = document.querySelector("#date_input");      // 新增行程 日期
const todoInput = document.querySelector("#todo_item_input"); // 新增行程 輸入內容
// const createModal = new bootstrap.Modal(
//   document.querySelector("#create_todo_modal")
// );
const createModal = bootstrap.Modal.getOrCreateInstance("#create_todo_modal"); // +

const updateModal = bootstrap.Modal.getOrCreateInstance("#update_todo_modal");// 整個更新模板 最大父層
const updateDateInput = document.querySelector("#date_update_input");      // 編輯行程 日期
const updateTodoInput = document.querySelector("#todo_item_update_input"); // 編輯行程 輸入內容
const deleteTodoBtn = document.querySelector(".delete-btn");               // 編輯行程 刪除
const updateTodoBtn = document.querySelector(".update-btn");               // 編輯行程 更新

const today = new Date();
let currentYear;
let currentMonth; //從1開始 1~12

const localStorageKey = "my-todo";
let todoItemObj = {}; // 要拿來當 localStorage.setItem( key , value ) 的 value
//--  今天按鈕  -------------------------------------------------
todayBtn.addEventListener("click", () => {
  initCalendar();
});
//--  上個月按鈕  -------------------------------------------------
lastMonthBtn.addEventListener("click", () => {
  currentMonth--;
  if (currentMonth < 1) {
    currentYear--;
    currentMonth = 12;
  }
  showTitle(currentYear, currentMonth);
  renderingCalendar();
});
//--  下個月按鈕  -------------------------------------------------
nextMonthBtn.addEventListener("click", () => {
  currentMonth++;
  if (currentMonth > 12) {
    currentYear++;
    currentMonth = 1;
  }
  showTitle(currentYear, currentMonth);
  renderingCalendar();
});
//--------  我是 + 按鈕  -------------------------------------------
document
  .querySelector("#create_todo_modal")
  .addEventListener("hidden.bs.modal", () => {
    dateInput.value = getDateStr(today);
    todoInput.value = "";
  });

// 閱讀日曆
function renderingCalendar() {
  // 這會是當年當月的第一天                       傳入年         ,傳入月-1  ,1代表當月的第一天  
  const firstDateOfCurrentMonth = new Date(currentYear, currentMonth - 1, 1);    // (2024,0,1)也就是(2024,1,1)
  // 這會是當年當月的最後一天
  const lastDateOfCurrentMonth = new Date(currentYear, currentMonth - 1 + 1, 0); // (2024,1,0)也就是(2024,1,31)
  /*
  -----------------------------------
  ♥ new Date
  最少參數數量為 0，這樣 new Date() 將創建一個包含當前日期和時間的對象。
  最多參數數量為 7，它們分別是年、月、日、時、分、秒、毫秒。如果超過這個數量，剩餘的參數將被忽略。
-----------------------------
  new Date( 年 , 月 , 日 );
  ♥ 年份(Year): 第1個參數表示年份
  可以是 4 位數字,例如 2022 
  可以是 2 位數字,例如 22。 如果省略 會默認當前的年份。
  ------------------------------
  ♥ 月份(Month): 第2個參數表示月份
  從 0 開始 (0 代表 1 月 ,1 代表 2 月，以此類推) ,最多到 11 (11 代表 12 月)。
  如果省略，默認是 0 ,即 1 月。
  ------------------------------
  ♥ 日期（Day）:
    第3個參數表示日期，即月份中的某一天，從 1 開始。
    如果省略，默認是 1。
  -----------------------------------
  */ 
  /** 日曆的第一格與當月1號的關係（1號星期幾，0-6）：
   * 0,1,2,3,4,5,6
   * 日 一 二 三 四 五 六
   * 1  2  3  4  5  6  7  --->(1-0)  星期日起始為  1
   * 0  1  2  3  4  5  6  --->(1-1)  星期日起始為  0
   *-1  0  1  2  3  4  5  --->(1-2)  星期日起始為 -1
   *-2 -1  0  1  2  3  4  --->(1-3)  星期日起始為 -2
   *-3 -2 -1  0  1  2  3  --->(1-4)  星期日起始為 -3
   *-4 -3 -2 -1  0  1  2  --->(1-5)  星期日起始為 -4
   *-5 -4  3 -2 -1  0  1  --->(1-6)  星期日起始為 -5
   */

  /** 日曆上顯示的最後一格與當月最後一天的關係(假如 f:30號)
   * 0,1,2,3,4,5,6
   * 日 一 二 三 四 五 六
   * f, 1, 2, 3, 4, 5, 6  --->(30 + (6 - 0))
   *  , f, 1, 2, 3, 4, 5  --->(30 + (6 - 1))
   *  ,  , f, 1, 2, 3, 4  --->(30 + (6 - 2))
   *  ,  ,  , f, 1, 2, 3  --->(30 + (6 - 3))
   *  ,  ,  ,  , f, 1, 2  --->(30 + (6 - 4))
   *  ,  ,  ,  ,  , f, 1  --->(30 + (6 - 5))
   *  ,  ,  ,  ,  ,  , f  --->(30 + (6 - 6))
   * .......
   */
  
  /* 以2024/1/1 ~ 2024/1/31 舉例來講 
  ♥ start = 1 - 禮拜1 = 0                                         ♥( 看上面:日曆的第一格與當月1號的關係 )
    那周就是下面這樣排列
    日 一 二 三 四 五 六
    0  1  2  3  4  5  6  --->(1-1)  0星期日
    -----------------------------------------------------------
  ♥ end = 取得當月最後一天是31號 + ( 6 - 當月最後一天星期3 ) = 34   ♥( 看上面:日曆上顯示的最後一格與當月最後一天的關係 )
    日 一 二 三 四 五 六
     ,  ,  , f, 1, 2, 3 --->(31 + (6 - 3))
  */ 
  let start = 1 - firstDateOfCurrentMonth.getDay(); // 禮拜幾  ,舉例來講是禮拜1,所以 1 - 1 = 0
  const end = lastDateOfCurrentMonth.getDate() + (6 - lastDateOfCurrentMonth.getDay()); // 34
  const dateArea = document.querySelector(".date-area"); // 不含星期幾橫列的 每日Body大面積
  dateArea.innerHTML = "";

  for (start; start <= end; start++) {                            // 當 0 < 34 的時候,0++ 會遍歷每個日曆小格子
    const curr = new Date(currentYear, currentMonth - 1, start); // 舉例的起始日為 ( 202,0,0 )也就是( 2023/12/31 )
    /* 來產生 <div class="border col"><span class="d-inline-block w-100 text-center"></span></div>
      ♥<div> 是每一格 各自日期的小格子
      ♥<span>是 小格子裡顯示幾號的小框框
    */
//--------  小格子樣式  ----------------------------------------------------------
    const dateDom = document.createElement("div");
    dateDom.classList.add("border", "col");
//--------  小日期樣式  ----------------------------------------------------------
    const dateEl = document.createElement("span");
    dateEl.classList.add("d-inline-block", "w-100", "text-center"); // 這邊是隨著遍歷 長出小格子<div><span>
//--------  小日期內容 幾號  ----------------------------------------------------------
    dateEl.textContent = curr.getDate();      // <span>的內容 = 起始日( 2023,12,31 )
//--------  小日期如果是當天,樣式的顏色上要看的出來是今天  -----------------------------
    if (
      curr.getFullYear() === today.getFullYear() && // 如果 curr 遍歷過程中 碰到是我打開月曆的當天的話
      curr.getMonth() === today.getMonth() &&       // 當天的小格子的 <span>要加上背景顏色  dateEl.classList.add
      curr.getDate() === today.getDate()
    ) {
      dateEl.classList.add("badge", "rounded-pill", "bg-warning");
    }
    dateDom.append(dateEl);

    //行事曆 Todo item 渲染
    const dateStr = getDateStr(curr);
    const currDateStr = dateStr;
    // todoItemObj = {
    //   "2024-01-01": ["todo1", "todo2"],
    //   "2023-12-31": ["跨年", "熬夜"],
    //   "2024-01-10":["sdhkjasd"]
    // }

//---------  把取得的年月日拿來當本地儲存的 key  ----------------------------------------------
    const currTodoItems = todoItemObj[currDateStr];
    if (currTodoItems) { // 如果代辦事項有內容的話,新增 代辦事項的<ul>
      //["todo1", "todo2"]
      const ul = document.createElement("ul");
//---------  藉由每個代辦事項的 key 來迭代相對應的 value  ----------------------------------
      currTodoItems.forEach((todo, idx) => { // 把每個代辦事項迭代出來 ,放進<li> ,加上 <li>的textContent
        const li = document.createElement("li");
        li.textContent = todo;
// ----------  當點到 某個代辦事項時  -------------------------------------------------
        //待辦事項click
        li.addEventListener("click", (e) => {
          updateDateInput.value = dateStr;
          updateTodoInput.value = todo;
//--------  ♥ 編輯行程 ♥ (更新.修改.刪除) 代辦事項的模板  -------------------------------
          updateModal.show();
//---------------  先做刪除功能  ----------------------------------
          //不能用 addEventListener 的原因：會重複註冊click，點開很多次編輯的話，點過的就都會刪除
          deleteTodoBtn.onclick = () => {
            deleteTodo(dateStr, idx);
          };
//----------------  做更新代辦事項功能,內容要去頭尾空白  -----------------------------------
          updateTodoBtn.onclick = () => {
            updateTodo(dateStr, idx, updateTodoInput.value.trim());
          };
// 這個 stopPropagation() 是 JavaScript 中事件對象的方法。這個方法的作用是停止事件的傳播，即阻止事件冒泡。
          e.stopPropagation();
        });

        ul.append(li);
      });
      dateDom.append(ul);
    }
//----------   ♥ 新增行程 ♥ 點小格子時,出現新增的 模板   ---------------------------------------------
    //dateDom 註冊 click 打開新增待辦事項modal, 日期為那個格子的日期
    dateDom.addEventListener("click", () => {
      dateInput.value = dateStr;
      createModal.show();
    });

    dateArea.append(dateDom);  // 在這裡才把小格子 <div> 加上去
  }
}
//-----------  更新 ♥編輯行程 代辦事項語法 要儲存本地  -------------------------------
function updateTodo(dateStr, idx, content) {
  const todoItemsOfDate = todoItemObj[dateStr];
  todoItemsOfDate[idx] = content;
  resetStorage();
  updateModal.hide();
  renderingCalendar();
}
//-----------  刪除 ♥編輯行程 代辦事項語法 要儲存本地  -------------------------------
function deleteTodo(dateStr, idx) {
  const todoItemsOfDate = todoItemObj[dateStr];

  todoItemsOfDate.splice(idx, 1);
  resetStorage();

  updateModal.hide();
  renderingCalendar();
}
// -----  本地儲存 Storage 的語法  -------------------------------------------
function resetStorage() {
  const jsonStr = JSON.stringify(todoItemObj); // 把 localStorage 的 value 轉成JSON檔 ,放進 jsonStr 裡面 
  localStorage.setItem(localStorageKey, jsonStr); // 設定 localStorage 的( key,value )
}
//------  顯示 年月日  ---------------------------------------------
function getDateStr(date) { // 取得月和日
  // return '2024-01-09'
  return `${date.getFullYear()}-${(date.getMonth() + 1)
    .toString()
    .padStart(2, "0")}-${date.getDate().toString().padStart(2, "0")}`;
}
// ---- 初始化日曆 --------------------------------------------------------------
function initCalendar() {
  currentYear = today.getFullYear();    // 2024
  currentMonth = today.getMonth() + 1;  // 1
  showTitle(currentYear, currentMonth); // 顯示( 2024 , 1)
  getTodoFromStorage();
  renderingCalendar();

  dateInput.value = getDateStr(today);
}
//---------  顯示標題  -----------------------------------------
function showTitle(year, month) {
  const monthNames = [
    'January', 'February', 'March', 'April', 'May', 'June',
    'July', 'August', 'September', 'October', 'November', 'December'
  ];

  const monthName = monthNames[month - 1]; // 月份從1開始，數組索引從0開始
  title.textContent = `${monthName}`;
}
// 把 JS物件 轉
function setTodoToStorage(dateStr, content) {
  if (!Array.isArray(todoItemObj[dateStr])) {
    todoItemObj[dateStr] = [];
  }

  todoItemObj[dateStr].push(content);
  // const obj = {
  //   "2024-01-01": ["todo1", "todo2"],
  //   "2023-12-31": ["跨年", "熬夜"],
  //   "2024-01-10":["sdhkjasd"]
  // };
  // let myObjKey = "2024-01-01"
  // obj[myObjKey]
  // let jsonStr = JSON.stringify(todoItemObj);
  // localStorage.setItem(localStorageKey, jsonStr);
  resetStorage();
}
//---------  取得舊紀錄的代辦事項  -----------------------------------------------
function getTodoFromStorage() { // 藉由 key 取得 存在本地儲存的代辦事項的value ,把他轉成 JS物件 ,放在todoObj
  const todoObj = JSON.parse(localStorage.getItem(localStorageKey));
  if (todoObj) { // if (todoObj) 是在檢查 todoObj 是否為真值，如果是 null 或 undefined 就會是 false
    todoItemObj = todoObj; // 如果是 true ,就把這個key的值放進 todoItemObj
  }
}
// -----  新增行程 的 新增按鈕  -----------------------------------
function createTodo() {
  //日期, 事項
  const dateString = dateInput.value;
  const todoContent = todoInput.value.trim();
  if (todoContent === "") {
    return;
  }
  //存進去
  setTodoToStorage(dateString, todoContent);

  createModal.hide();
  renderingCalendar();
}

initCalendar();
