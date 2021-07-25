
var container = document.getElementById("bombField")
container.style.display = "none"

var menu = document.getElementById('menu')
var button = document.createElement('button')
var menuForm = document.getElementById("menuForm")
var submit = document.getElementById("submit")

var restartButton = document.createElement("button")
restartButton.id = "restartButton"
restartButton.innerHTML = "Restart Game"

var minePos
var winCounter
var timer
var defaultBoard = {
  board: {
    minePositions: [
      [1, 3],
      [3, 0],
      [4, 2],
      [4, 5],
      [4, 7],
      [6, 9],
      [7, 7],
      [8, 9],
      [9, 3],
      [9, 9]
    ],
    rows: 10,
    cols: 10,
    mines: 10
  }
}



function doAjax() {
  var rows = document.getElementById("Rows")
  var columns = document.getElementById("Columns")
  var mines = document.getElementById("Mines")
  var url = 'http://veff213-minesweeper.herokuapp.com/api/v1/minesweeper';



  //Perform an AJAX POST request to the url, and set the param 'myParam' in the request body to paramValue
  axios.post(url, { mines: mines.value, cols: columns.value, rows: rows.value })
    .then(function (response) {
      generateBoard(response.data)
    })
    .catch(function (error) {
      //when response is 400 error, display default board
      generateBoard(defaultBoard)
    })

}

function toggleTimer(toggle) {
  function incrementTimer() {
    seconds++
    if (seconds == 60) {
      minutes++
      seconds = 0
    }
    var timeString = ""
    if (seconds < 10) {
      timeString = minutes + ":0" + seconds
    }
    else {
      timeString = minutes + ":" + seconds
    }
    document.getElementById("clock").innerHTML = timeString
  }
  var seconds = 0
  var minutes = 0
  if (toggle == true) {

    document.getElementById("clock").innerHTML = "0:00"
    document.getElementById("timer").style.display = "inline-block"
    timer = setInterval(incrementTimer, 1000);
  }
  else {
    clearInterval(timer)
  }
}

function generateBoard(data) {
  toggleTimer(true)

  menuForm.style.display = "none"
  document.getElementById("running").style.display = "block"

  container.innerHTML = ""
  minePos = data.board.minePositions

  rows = data.board.rows
  columns = data.board.cols
  mines = data.board.mines
  map = mapCreator(rows, columns)

  winCounter = (rows * columns) - mines

  container.style.display = "block"
  menu.style.width = 100 + (32 * columns) + "px";

  calculateTileScore(map, minePos, columns, rows)

  generateButtons(map, rows, columns)

  addFieldButtonListeners()
}

function mapCreator(rows, columns) {
  var map = Array()

  for (var y = 0; y < rows; y++) {
    var columnArray = Array()
    for (var x = 0; x < columns; x++) {
      columnArray.push(0)
    }
    map.push(columnArray)
  }
  return map
}

function calculateTileScore(map, minePos, columns, rows) {
  for (var x = 0; x < minePos.length; x++) {
    var xPos = minePos[x][1]
    var yPos = minePos[x][0]
    var columnVal = columns
    var rowsVal = rows
    map[yPos][xPos] = -1
    var north = yPos - 1
    var south = yPos + 1
    var west = xPos - 1
    var east = xPos + 1

    //for every button around each bomb we call calculateScoreForPosition 
    //with relevant parameters which then checks if the button position would be out of bounds or a bomb
    //before incrementing its score

    //North 
    calculateScoreForPosition(map, north, xPos, [
      { leftValue: north, rightValue: 0, biggerOrEqualThan: true }
    ])
    // North-East
    calculateScoreForPosition(map, north, east, [
      { leftValue: north, rightValue: 0, biggerOrEqualThan: true },
      { leftValue: east, rightValue: columnVal, biggerOrEqualThan: false }
    ])
    // East
    calculateScoreForPosition(map, yPos, east, [
      { leftValue: east, rightValue: columnVal, biggerOrEqualThan: false }
    ])
    // South-East
    calculateScoreForPosition(map, south, east, [
      { leftValue: south, rightValue: rowsVal, biggerOrEqualThan: false },
      { leftValue: east, rightValue: columnVal, biggerOrEqualThan: false }
    ])
    // South
    calculateScoreForPosition(map, south, xPos, [
      { leftValue: south, rightValue: rowsVal, biggerOrEqualThan: false }
    ])
    // South-West
    calculateScoreForPosition(map, south, west, [
      { leftValue: south, rightValue: rowsVal, biggerOrEqualThan: false },
      { leftValue: west, rightValue: 0, biggerOrEqualThan: true }
    ])
    // West
    calculateScoreForPosition(map, yPos, west, [
      { leftValue: west, rightValue: 0, biggerOrEqualThan: true }
    ])
    // North-West
    calculateScoreForPosition(map, north, west, [
      { leftValue: north, rightValue: 0, biggerOrEqualThan: true },
      { leftValue: west, rightValue: 0, biggerOrEqualThan: true }
    ])
  }
}

function calculateScoreForPosition(
  map,
  yPos,
  xPos,
  comparisons
) {
  for (var comparison in comparisons) {

    if (comparisons[comparison].biggerOrEqualThan == true) {
      if (comparisons[comparison].leftValue < comparisons[comparison].rightValue) {
        return
      }
    }

    else {
      if (comparisons[comparison].leftValue >= comparisons[comparison].rightValue) {
        return
      }
    }

    if (map[yPos][xPos] == -1) {
      return
    }
  }
  map[yPos][xPos]++
}

function generateButtons(map, rows, columns) {

  function assignDP(mapReturn) {
    var imgElem = document.createElement('img')

    if (mapReturn == -1) {
      imgElem.src = "bomb.png"
      imgElem.style.width = "100%"
      return imgElem
    }
    else if (mapReturn == 0) {
      number = document.createElement('span')
      return number
    }
    else {
      number = document.createElement('span')
      number.innerHTML = mapReturn
      if (mapReturn === 1){
        number.style.color = "blue"
      }
      else if (mapReturn === 2) {
        number.style.color = "green"
      }
      else {
        number.style.color = "red"
      }
      return number
    }
  }

  for (var y = 0; y < rows; y++) {
    var innerContainer = document.createElement('div')

    for (var x = 0; x < columns; x++) {

      var butt = document.createElement('button')

      buttSpan = document.createElement('span')
      buttSpan.id = "buttSpanId" + y + "," + x
      buttSpan.className = "buttSpanClass"
      buttSpan.appendChild(assignDP(map[y][x]))

      imgElem = document.createElement('img')
      imgElem.src = 'flag.png'
      imgElem.style.width = '100%'
      imgElem.id = 'flagId' + y + ',' + x
      imgElem.className = "flags"
      imgElem.style.display = 'None'

      butt.appendChild(buttSpan)
      butt.appendChild(imgElem)
      butt.id = y + "," + x

      butt.className = "fieldButton"
      innerContainer.appendChild(butt)
    }
    innerContainer.className = "innerContainer"
    container.appendChild(innerContainer)
  }

}

function addFieldButtonListeners() {
  var fieldButtons = document.getElementsByClassName("fieldButton")

  for (var x = 0; x < fieldButtons.length; x++) {
    fieldButtons[x].addEventListener('contextmenu', contextMenuClick)
    fieldButtons[x].addEventListener('click', leftClick)
  }
}

function removeFieldButtonListeners() {
  buttons = document.getElementsByClassName("fieldButton")
  for (var i = 0; i < buttons.length; i++) {

    buttons[i].removeEventListener('click', leftClick)
    buttons[i].removeEventListener('contextmenu', contextMenuClick)
    buttons[i].addEventListener('contextmenu', function (event) {
      event.preventDefault();
    })
  }
}

function checkTile(id, map) {
  idArr = id.split(",")
  return map[idArr[0]][idArr[1]]
}

function checkTileValue(tileValue) {
  if (tileValue == -1) {
    lose(tileValue)
    
  }
  else if (tileValue == 0) {
    revealTile(id, map)
    
  }
}

function leftClick(event) {
  id = event.target.id

  if (document.getElementById(id).disabled == true) {
    return
  }

  if (document.getElementById("flagId" + id).style.display == 'block') {

    document.getElementById(id).disabled = false
    return
  }
  document.getElementById(id).disabled = true

  document.getElementById("buttSpanId" + id).style.display = "block"

  tileValue = checkTile(id, map)

  checkTileValue(tileValue)

  if (tileValue != -1){
    winCounter--
  }
  checkWin(minePos)
}

function contextMenuClick(event) {

  event.preventDefault()
  id = event.target.id

  flag = document.getElementById("flagId" + id)

  if (flag.style.display == 'none') {
    flag.style.display = "block"
  }
  else {
    flag.style.display = 'none'
  }
}

function checkLegal(coordinates, map) {

  var x = map.length;// is rows
  var y = map[0].length; // is columns
  var coY = coordinates[1]
  var coX = coordinates[0]

  if (coY >= 0 && coX >= 0 && coY < y && coX < x) {

    return true
  }

  else {

    return false
  }
}

async function revealTile(id, map) {

  document.getElementById(id).disabled = true
  var coordinates = id.split(",")
  var xPos = parseInt(coordinates[1])
  var yPos = parseInt(coordinates[0])
  var east = [yPos, xPos + 1]
  var west = [yPos, xPos - 1]
  var south = [yPos + 1, xPos]
  var north = [yPos - 1, xPos]
  var southEast = [yPos + 1, xPos + 1]
  var southWest = [yPos + 1, xPos - 1]
  var northEast = [yPos - 1, xPos + 1]
  var northWest = [yPos - 1, xPos - 1]

  if (checkLegal(east, map) && document.getElementById(east).disabled != true) {
    setTimeout(() => {
      document.getElementById(east).click()
    }, 5);
  }

  if (checkLegal(west, map) && document.getElementById(west).disabled != true) {
    setTimeout(() => {
      document.getElementById(west).click()
    }, 5);
  }

  if (checkLegal(south, map) && document.getElementById(south).disabled != true) {
    setTimeout(() => {
      document.getElementById(south).click()
    }, 50);
  }

  if (checkLegal(north, map) && document.getElementById(north).disabled != true) {
    setTimeout(() => {
      document.getElementById(north).click()
    }, 50);
  }

  if (checkLegal(northEast, map) && document.getElementById(northEast).disabled != true) {
    setTimeout(() => {
      document.getElementById(northEast).click()
    }, 50);
  }

  if (checkLegal(northWest, map) && document.getElementById(northWest).disabled != true) {
    setTimeout(() => {
      document.getElementById(northWest).click()
    }, 50);
  }

  if (checkLegal(southEast, map) && document.getElementById(southEast).disabled != true) {
    setTimeout(() => {
      document.getElementById(southEast).click()
    }, 50);
  }

  if (checkLegal(southWest, map) && document.getElementById(southWest).disabled != true) {
    setTimeout(() => {
      document.getElementById(southWest).click()
    }, 50);
  }
}

function checkWin() {
  if (winCounter == 0) {
    toggleTimer(false)
    document.getElementById("win").appendChild(restartButton)

    for (var i = 0; i < minePos.length; i++) {
      var id = minePos[i][0] + "," + minePos[i][1]

      document.getElementById("flagId" + id).style.display = "block"
    }
    document.getElementById("running").style.display = "none"
    document.getElementById("win").style.display = "block"
    removeFieldButtonListeners()
  }
}

async function lose() {
  toggleTimer(false)

  document.getElementById("running").style.display = "none"
  document.getElementById("lose").style.display = "block"
  document.getElementById("lose").appendChild(restartButton)

  for (var i = 0; i < minePos.length; i++) {
    var id = minePos[i][0] + "," + minePos[i][1]
    document.getElementById("flagId" + id).style.display = "None"
    document.getElementById("buttSpanId" + id).style.display = "Block"
    document.getElementById(id).style.backgroundColor = "#FC8074"

  }
  removeFieldButtonListeners()
  flags = document.getElementsByClassName("flags")
  for(var i = 0; i < flags.length; i++){
    flags[i].style.display = "none"
  }
  winCounter = minePos.length

}

function restartGame(event) {
  toggleTimer(false)
  event.preventDefault()
  menuForm.style.display = "inline-block"
  document.getElementById("timer").style.display = "none"
  document.getElementById("running").style.display = "none"
  document.getElementById("win").style.display = "none"
  document.getElementById("lose").style.display = "none"
  removeFieldButtonListeners()
}

submit.addEventListener('click', function (event) {
  event.preventDefault();

  document.getElementById("running").appendChild(restartButton)

  doAjax()
})

restartButton.addEventListener('click', restartGame)
