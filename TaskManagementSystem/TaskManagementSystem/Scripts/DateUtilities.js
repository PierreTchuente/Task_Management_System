//variables
var Months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];


// get month and day of a date

function getMonthDayYear(date) {
    return Months[date.getMonth()] + " " + date.getDate() + ", " + date.getFullYear();
}


function getMonthDayYear2(date) {
    return date.getDate() + "-" + Months[date.getMonth()] + "-" + date.getFullYear();
}


function getMonthDayYear3(date) {
    return date.getFullYear() + "-" + (parseInt(date.getMonth()) + 1) + "-" + date.getDate();
}


function getFullDateTime(date) {
    return Months[date.getMonth()] + " " + date.getDate() + ", " + date.getFullYear() + " at " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
}

function getFullDateTimeForCalendar(date) {
    var month = "";
    var day = "";
    var hours = "";
    var minute = "";
    var second = "";
    if ((date.getMonth() + 1) < 10) {
        month = "0" + (date.getMonth() + 1);
    }
    else {
        month = (date.getMonth() + 1);
    }


    if (date.getDate() < 10) {
        day = "0" + date.getDate();
    }
    else {
        day = date.getDate();
    }


    if (date.getHours() < 10) {
        hours = "0" + date.getHours();
    }
    else {
        hours = date.getHours();
    }


    if (date.getMinutes() < 10) {
        minute = "0" + date.getMinutes();
    }
    else {
        minute = date.getMinutes();
    }

    if (date.getSeconds() < 10) {
        second = "0" + date.getSeconds();
    }
    else {
        second = date.getSeconds();
    }



    return date.getFullYear() + "-" + month + "-" + day + "T" + hours+ ":" + minute + ":" + second;
}



function getFullDateTimeForCalendarver2(date) {
    var month = "";
    var day = "";
    var hours = "";
    var minute = "";
    var second = "";
    if ((date.getMonth() + 1) < 10) {
        month = "0" + (date.getMonth() + 1);
    }
    else {
        month = (date.getMonth() + 1);
    }


    if (date.getDate() < 10) {
        day = "0" + date.getDate();
    }
    else {
        day = date.getDate();
    }


    if (date.getHours() < 10) {
        hours = "0" + date.getHours();
    }
    else {
        hours = date.getHours();
    }


    if (date.getMinutes() < 10) {
        minute = "0" + date.getMinutes();
    }
    else {
        minute = date.getMinutes();
    }

    if (date.getSeconds() < 10) {
        second = "0" + date.getSeconds();
    }
    else {
        second = date.getSeconds();
    }



    return date.getFullYear() + "-" + month + "-" + day + "T" + hours + ":" + minute;
}



function getFullTime(date) {
    return date.getHours() + ":" + date.getMinutes();
}

function daysBetween(date1, date2) {
    //Get 1 day in milliseconds
    var one_day = 1000 * 60 * 60 * 24;

    // Convert both dates to milliseconds
    var date1_ms = date1.getTime();
    var date2_ms = date2.getTime();

    // Calculate the difference in milliseconds
    var difference_ms = date2_ms - date1_ms;

    // Convert back to days and return
    return Math.round(difference_ms / one_day);
}

//function FrommmddyyyyhhmmssamTOyyyyddmmThhmmss(date) {
//    debugger;

//    var datetimespart = date.split(" ");
//    var datepart = datetimespart[0].split("/");
//    var month = "";
//    var day = "";
//    if ((datepart[0]).length < 2){
//        month = "0" + datepart[0];
//    } else {
//        month = datepart[0];
//    }
//    if ((datepart[1]).length < 2){
//        day = "0" + datepart[1];
//    }
//    else {
//        day = datepart[1];
//    }

//    timepart = datetimespart[1].split(":")
//    hour = "";
//    min = "";
//    sec = "";

//    if ((timepart[0]).length < 2) {
//        hour = "0" + timepart[0];
//    }
//    else {
//        hour = timepart[0];
//    }

//    if ((timepart[1]).length < 2) {
//        min = "0" + timepart[1];
//    }
//    else {
//        min = timepart[1];
//    }

//    if ((timepart[2]).length < 2) {
//        sec = "0" + timepart[2];
//    }
//    else {
//        sec = timepart[2];
//    }

//    return datepart[2] + "-" + month + "-" +day + "T" + hour + ":" + min + ":" + sec;
//}