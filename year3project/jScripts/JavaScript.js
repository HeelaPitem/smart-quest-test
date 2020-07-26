var timeLeft = 29;
var DepScores = new Array();
var DepNames = new Array();
var isPaused = false;
var timerId;
var continueOn;

$(document).ready(function () {

    //הוצאת מספר הקונטייר שהמשתמש לחץ עליו
    var ContainerID = document.getElementById('NavHiddenField').value;
    console.log("UserLoggedInHiddenField.value: " + UserLoggedInHiddenField.value);
    console.log("Navhiddenvalue: " + NavHiddenField.value);

    //החבאת כל הקונטיירנים בטעינת הדף
    $(".container").hide();

    //לולאה שרצה על כל התמונות בתפריט הניווט ומשנה את העיצוב 
    $(".navBtnImage").each(function (index, value) {
        // console.log($(this).attr("itemid"));

        $(this).css({ "height": "25px", "width": "25px" });

        if (ContainerID == $(this).attr("imageNum")) {
            $(this).css({ "height": "35px", "width": "35px" });
        }
    });

    //לולאה שרצה על כל התפריט הניווט ומשנה אותם למצה פעיל או לא 
    $(".navBtn").each(function (index, value) {
        if (UserLoggedInHiddenField.value == "true") {
            $(this).css({ "pointer-events": "auto", "opacity": "1" });
            $("#SignUpBtn").hide();
            $("#SignInLink").hide();
            $("#profileBottomDiv").css("display", "block");
            $("#infoLabel").hide();
        }
        else {
            $(this).css({ "pointer-events": "none", "opacity": "0.3" });
        }
    });

    var AnswerHiddenField = document.getElementById('AnswerHiddenField').value;
    console.log("AnswerHiddenField: " + AnswerHiddenField);


    if (ContainerID == "01") {
        $("#profileContainer").show();  //הצגת הקונטייר כניסה למשתמש רשום במערכת
        $("#emailSignInDiv").show();  //הצגת הקונטייר כניסה למשתמש רשום במערכת
        $("#proflieNavImage").css({ "height": "35px", "width": "35px" }); //הגדלת אייקון הפרופיל בתפריט ניווט
        $("#profileLinkButton").css({ "pointer-events": "auto", "opacity": "1" }); //החזרת פרופיל בתפריט ניווט ללינק פעיל
    }
    else if (ContainerID == "1") {
        $("#howtoplayContainer").show(); //הצגת קונטיינר איך משחקים
    }

    else if (ContainerID == "2") {
        $("#preGameContainer").show();  //הצגת הקונטייר של לפני המשחק- בחירת קטגוריה
    }

    else if (ContainerID == "GameStarted") {
        $("#gameContainer").show(); //הצגת קונטיינר שח המשחק עצמוeach
        $("#gameNavImage").css({ "height": "35px", "width": "35px" }); //הגדלת אייקון הפרופיל בתפריט ניווט


        //הגדרת טיימר למשחק
        var elem = document.getElementById('CountDownLabel');
        timerId = setInterval(function () {
            if (!isPaused) {

                if (timeLeft == 0) { //במידה ונגמר הזמן
                    clearTimeout(timerId);
                    $("#StarIcon3").css('opacity', '0.3');
                    $("#TimeEndContainer").show();
                    $("#gameContainer").hide();
                }
                else if (20 >= timeLeft && timeLeft >= 11) { //במידה ונשאר בין 11-20 שניות
                    $("#StarIcon1").css('opacity', '0.3');
                    console.log("timeLeft: " + timeLeft);
                    elem.innerHTML = timeLeft;
                    timeLeft--;
                }
                else if (10 >= timeLeft) { //במידה ונשאר בין 1-10 שניות
                    $("#StarIcon2").css('opacity', '0.3');
                    console.log("timeLeft: " + timeLeft);
                    elem.innerHTML = timeLeft;
                    timeLeft--;
                }
                else {
                    elem.innerHTML = timeLeft; //הדפסת מספר השניות שנותרו
                    timeLeft--;
                }

            }
        }, 1000);

    }
    else if (ContainerID == "GameFeedback") {
        $("#feedbackContainer").show(); //הצגת קונטיינר משוב לשאלה   
        $("#gameNavImage").css({ "height": "35px", "width": "35px" }); //הגדלת אייקון הפרופיל בתפריט ניווט
    }
    else if (ContainerID == "3") {  //הצגת קונטיינר תוצאות
        $("#scoreContainer").show();
        loadCharts();
    }
    else if (ContainerID == "4") {
        $("#moreContainer").show();  //הצגת הקונטייר של עוד
    }
    else {
        $("#profileContainer").show();        //הצגת הקונטייר של עמוד כניסה
        $("#proflieNavImage").css({ "height": "35px", "width": "35px" }); //הגדלת אייקון הפרופיל בתפריט ניווט
        $("#profileLinkButton").css({ "pointer-events": "auto", "opacity": "1" }); //החזרת פרופיל בתפריט ניווט ללינק פעיל
        $("#emailSignUpDiv").show();  //הצגת הקונטייר כניסה למשתמש רשום במערכת
    }




    //כאשר מתבצע לחיצה על אחד מכפתורי הניווט
    $('.navBtn').click(function () {

        //הוצאת מספר הקונטיינר שנלחץ
        var conNum = $(this).attr("containernum");
        document.getElementById('NavHiddenField').value = conNum;

    });

    $('#chooseCatDDL').change(function () {
        $('#catFinishedLabel').text('');
        console.log("returning false");
        return false;
    });

    //לחיצה על כפתור התחל משחק - לאחר שקטגוריה נבחרה
    $('#startGameButton').click(function () {

        document.getElementById('NavHiddenField').value = "GameStarted";
        console.log("java click ran + hidden field: " + document.getElementById('HiddenField1').value);

    });

    //לחיצה על כפתור המשך למשחק (לאחר איך משחקים)
    $('#navToGameBtn').click(function () {
        document.getElementById('NavHiddenField').value = "2";
    });

    //לחיצה על אחד מכפתורי המשחק - חיוני או לא חיוני
    $('.gameBtns').click(function () {

        document.getElementById('NavHiddenField').value = "GameFeedback";

        //הכנסת ערך של מספר שניות שלקח למשתמש כדי לענות על מקרה אל תוך שדה מוחבא
        var curremtTime = (30 - timeLeft);
        document.getElementById('SecondsHiddenField').value = curremtTime;

    });

    //לחיצה על כפתור היכנס בתור משתמש רשום (בפרופיל)
    $('#SignInLink').click(function () {
        $("#emailSignUpDiv").hide();
        $("#emailSignInDiv").show();
        $("#SignUpLink").removeClass("bounce-4");

        return false;
    });

    //לחיצה על כפתור הירשם בתור משתמש חדש (בפרופיל)
    $('#SignUpLink').click(function () {
        $("#emailSignInDiv").hide();
        $("#emailSignUpDiv").show();
        $("#SignInLink").removeClass("bounce-4");

        return false;
    });


    //לחיצה על כפתור השהייה במשחק
    $('#PauseImageButton').click(function () {

        if (isPaused == false) {

            $(".gameBtns").prop('disabled', true);//הגדרת כפתורי המשחק למצב לא פעיל
            $("#mySwiper").hide(); //הסתרת התוכן של פרטי המטופל
            $("#bgSwiperDiv").hide(); //הסתרת הרקע שמשמש לקליטת פעולה החלקה עם באצבע
            $("#ContinueLinkButton").css('display', 'block'); //הצגת כיתוב המאפשר לחזור למשחק

            this.src = "images/play.svg"; //שינוי התמונה לאייקון המשך משחק

            isPaused = true;
        }
        else {

            $(".gameBtns").prop('disabled', false);//הגדרת כפתורי המשחק למצב פעיל
            $("#mySwiper").show(); //הצגת התוכן של פרטי המטופל
            $("#bgSwiperDiv").show(); //הצגת הרקע שמשמש לקליטת פעולה החלקה עם באצבע
            $("#ContinueLinkButton").hide();//הצגת כיתוב המאפשר לחזור למשחק

            this.src = "images/pause.svg"; //שינוי התמונה לאייקון שהייה

            isPaused = false;
        }

        return false;

    });

    //כפתור ביטול כפתור ההשהייה - המשך במשחק
    $('#ContinueLinkButton').click(function () {

        $('#PauseImageButton').click();

        return false;
    });

    //-------------------------------------------------ספירת תווים------------------------------//

    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
    });


    //פונקציה שמקבלת את תיבת הטקסט שבה מקלידים ובודקת את מספר התווים
    function checkCharacter(myTextBox) {

        //משתנה המקבל את מספר תיבת הטקסט 
        var connectBtnAttr = myTextBox.attr("connectBtn");

        //משתנה ששומר את מספר תיבות הטקסט שמחוברות לאותו הכפתור
        var TextBoxsCounter = 0;

        //משתנה ששומר את מספר התיבות טקסט שיש בתוכם לפחות תו אחד
        var TextBoxsLength = 0;

        //פונקציה שעוברת על כל הרכיבים עם קלאס CharacterCount
        $(".CharacterCount").each(function () {
            if ($(this).attr("connectBtn") == connectBtnAttr) {
                TextBoxsCounter++; //ספירת תיבות הטקסט שמחוברת לאותו הכפתור

                if ($(this).val().length > 0) {
                    TextBoxsLength++; //ספירת תיבות הטקסט שבהם יש תו אחד לפחות
                }

            }
        });

        //תנאי שבודק אם מספר התיבות טקסט זהה מספר התיבות טקסט שיש בהם לפחות תו אחד
        if (TextBoxsCounter == TextBoxsLength) {
            document.getElementById(myTextBox.attr("connectBtn")).disabled = false; //הפיכת הכפתור למצב פעיל
        }
        else {
            document.getElementById(myTextBox.attr("connectBtn")).disabled = true; //הפיכת הכפתור למצב לא פעיל
        }

    }

});




    //-----------------------------הטענת גרפים לעמוד תוצאות--------------------------------//

    function loadCharts() {

        var answeredCases = $("#AnsweredCasesHiddenField").val();
        var sumCases = $("#CasesCountHiddenField").val();
        var leftCase = (sumCases - answeredCases);


        var ctx = document.getElementById('myChart').getContext('2d');
        console.log(DepScores);


        var chart = new Chart(ctx, {
            // The type of chart we want to create
            //type: 'line',
            type: 'doughnut',


            // The data for our dataset
            data: {
                datasets: [{
                    data: [answeredCases, leftCase],
                    backgroundColor: ['rgb(255, 99, 132)',
                        'rgb(255, 200, 132)']
                }]

                // These labels appear in the legend and in the tooltips when hovering different arcs

            },


            // Configuration options go here
            options: {
                legend: {
                    display: false,
                }

            }
        });

    }


    //-----------------------------קוד שאחראי לפעולת החלקה swipe--------------------------------//


    // TOUCH-EVENTS SINGLE-FINGER SWIPE-SENSING JAVASCRIPT
    // Courtesy of PADILICIOUS.COM and MACOSXAUTOMATION.COM

    // this script can be used with one or more page elements to perform actions based on them being swiped with a single finger

    var triggerElementID = null; // this variable is used to identity the triggering element
    var fingerCount = 0;
    var startX = 0;
    var startY = 0;
    var curX = 0;
    var curY = 0;
    var deltaX = 0;
    var deltaY = 0;
    var horzDiff = 0;
    var vertDiff = 0;
    var minLength = 72; // the shortest distance the user may swipe
    var swipeLength = 0;
    var swipeAngle = null;
    var swipeDirection = null;

    // The 4 Touch Event Handlers

    // NOTE: the touchStart handler should also receive the ID of the triggering element
    // make sure its ID is passed in the event call placed in the element declaration, like:
    // <div id="picture-frame" ontouchstart="touchStart(event,'picture-frame');"  ontouchend="touchEnd(event);" ontouchmove="touchMove(event);" ontouchcancel="touchCancel(event);">

    function touchStart(event, passedName) {
        // disable the standard ability to select the touched object
        //event.preventDefault();
        // get the total number of fingers touching the screen
        fingerCount = event.touches.length;
        // since we're looking for a swipe (single finger) and not a gesture (multiple fingers),
        // check that only one finger was used
        if (fingerCount == 1) {

            // get the coordinates of the touch
            startX = event.touches[0].pageX;
            startY = event.touches[0].pageY;
            // store the triggering element ID
            triggerElementID = passedName;
        } else {
            // more than one finger touched so cancel
            //touchCancel(event);
        }
    }

    function touchMove(event) {
        // event.preventDefault();

        console.log("touchmove");


        if (event.touches.length == 1) {
            curX = event.touches[0].pageX;
            curY = event.touches[0].pageY;
            if (fingerCount == 1) {
                // use the Distance Formula to determine the length of the swipe
                swipeLength = Math.round(Math.sqrt(Math.pow(curX - startX, 2) + Math.pow(curY - startY, 2)));
                console.log("one finger pressed");

                //שליפת מיקום של גלרית פרטי המטופל
                var rect = document.getElementById("mySwiper").getBoundingClientRect();

                // if the user swiped more than the minimum length, perform the appropriate action
                if ((swipeLength >= minLength) && ((startY > (rect.bottom + 10)) || (startY < (rect.top - 10)))) {
                    console.log("swipe more than minimum");
                    console.log("swipe length: " + swipeLength);


                    //קריאה לפונקציה המחשבת את כיוון הswipe
                    caluculateAngle();
                    determineSwipeDirection();




                    var directionImg;

                    //תנאי הבודק לאיזה כיוון המשתמש החליק
                    if (swipeDirection == 'left') {

                        directionImg = $('#dontsendImg');

                        //לשנות צבעי הרקע לאפור
                        $('#sendBtn').prop('disabled', true);

                    }

                    if (swipeDirection == 'right') {

                        directionImg = $('#sendImg');

                        //לשנות צבעי הרקע לאפור
                        $('#dontSendBtn').prop('disabled', true);

                    }



                    directionImg.show();
                    directionImg.css("opacity", 0.2);
                    directionImg.css("width", "70%");


                    if (swipeLength >= 100) {
                        directionImg.css("opacity", 0.7);
                        directionImg.css("width", "80%");
                    }

                    if (swipeLength >= 150) {
                        directionImg.css("opacity", 1);
                        directionImg.css("width", "90%");
                    }
                    directionImg.show();




                }
            }

        } else {
            touchCancel(event);
        }
    }

    function touchEnd(event) {
        // event.preventDefault();
        // check to see if more than one finger was used and that there is an ending coordinate
        if (fingerCount == 1 && curX != 0) {
            // use the Distance Formula to determine the length of the swipe
            swipeLength = Math.round(Math.sqrt(Math.pow(curX - startX, 2) + Math.pow(curY - startY, 2)));


            var rect = document.getElementById("mySwiper").getBoundingClientRect();
            console.log("top: " + rect.top + "right: " + rect.right + "bottom: " + rect.bottom + "left: " + rect.left);

            //החבאת תמונות לאחר סיום לחיצה
            $('#sendImg').hide();
            $('#dontsendImg').hide();

            //להחזיר את צבע הכפתורים לקדמותם
           // $('#sendBtn').css({ "background-color": "white !important", "color": "blue !important", "box-shadow": "0px 3px 20px #c5c5c5 !important" });
            $('#sendBtn').prop('disabled', false);
            $('#dontSendBtn').prop('disabled', false);


            // if the user swiped more than the minimum length, perform the appropriate action
            if ((swipeLength >= minLength) && ((startY > (rect.bottom + 10)) || (startY < (rect.top - 10)))) {
                caluculateAngle();
                determineSwipeDirection();
                processingRoutine();
                touchCancel(event); // reset the variables


            } else {
                touchCancel(event);
            }
        } else {
            touchCancel(event);
        }
    }

    function touchCancel(event) {
        // reset the variables back to default values
        fingerCount = 0;
        startX = 0;
        startY = 0;
        curX = 0;
        curY = 0;
        deltaX = 0;
        deltaY = 0;
        horzDiff = 0;
        vertDiff = 0;
        swipeLength = 0;
        swipeAngle = null;
        swipeDirection = null;
        triggerElementID = null;
    }

    function caluculateAngle() {
        var X = startX - curX;
        var Y = curY - startY;
        var Z = Math.round(Math.sqrt(Math.pow(X, 2) + Math.pow(Y, 2))); //the distance - rounded - in pixels
        var r = Math.atan2(Y, X); //angle in radians (Cartesian system)
        swipeAngle = Math.round(r * 180 / Math.PI); //angle in degrees
        if (swipeAngle < 0) { swipeAngle = 360 - Math.abs(swipeAngle); }
    }

    function determineSwipeDirection() {
        if ((swipeAngle <= 45) && (swipeAngle >= 0)) {
            swipeDirection = 'left';
        } else if ((swipeAngle <= 360) && (swipeAngle >= 315)) {
            swipeDirection = 'left';
        } else if ((swipeAngle >= 135) && (swipeAngle <= 225)) {
            swipeDirection = 'right';
        } else if ((swipeAngle > 45) && (swipeAngle < 135)) {
            swipeDirection = 'down';
        } else {
            swipeDirection = 'up';
        }
    }

    function processingRoutine() {
        var swipedElement = document.getElementById(triggerElementID);
        if (swipeDirection == 'left') {
            // REPLACE WITH YOUR ROUTINES
            //    swipedElement.style.backgroundColor = 'orange';
            console.log("swiped");
            $('#dontSendBtn').click();


        } else if (swipeDirection == 'right') {
            // REPLACE WITH YOUR ROUTINES
            //   swipedElement.style.backgroundColor = 'green';
            console.log("swiped");
            $('#sendBtn').click();

        } else if (swipeDirection == 'up') {
            // REPLACE WITH YOUR ROUTINES
            //  swipedElement.style.backgroundColor = 'maroon';
            console.log("swiped");

        } else if (swipeDirection == 'down') {
            // REPLACE WITH YOUR ROUTINES
            console.log("swiped");
            //    swipedElement.style.backgroundColor = 'purple';
        }
    }