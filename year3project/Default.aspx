<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Smart-Quest</title>
    <meta name="description" content="משחק בשביל מתמחים" />
    <meta name="keywords" content=",מחלות במשחק, בדיקות במשחק, דוקטור במשחק, מטופל במשחק" />
    <meta name="author" content="הילה פיטם ושירה פיקר" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=yes" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <%--CSS--%>
    <link href="Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="Styles/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="https://unpkg.com/swiper/swiper-bundle.css" rel="stylesheet" />
    <link href="https://unpkg.com/swiper/swiper-bundle.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="icon" type="image/svg" href="/images/flasks.svg" />

    <%--Scripts--%>
    <script src="https://unpkg.com/swiper/swiper-bundle.js"></script>
    <script src="https://unpkg.com/swiper/swiper-bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js@2.8.0"></script>
    <script src="jScripts/jquery-2.1.1.min.js"></script>
    <script src='http://code.jquery.com/jquery-3.3.1.slim.min.js'></script>
    <script src='/path/to/jquery.keyframes[.min].js'></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="jscripts/JavaScript.js"></script>

    <%--    <script src="jScripts/jquery-1.7.1.min.js" type="text/javascript"></script>--%>
</head>
<body>

    <form runat="server">

        <header>

            <div id="logoContainer">

                <img src="~/images/flasks.svg" id="logoImage" runat="server" />
                <h1>Smart-Quest</h1>
                <img src="~/images/hitlogo.jpg" id="HitLogo" runat="server" />

            </div>

        </header>




        <%-------------- עמוד פרופיל---------------%>

        <div id="profileContainer" class="container" containerid="0" runat="server">

            <div id="profileTopDiv" runat="server">
            </div>


            <%-------------- דיב פנימי לכניסה למשתמשים חדשים---------------%>

            <div id="emailSignUpDiv" class="userInfoInput container">


                <label for="fullNameTB">שם מלא</label>

                <asp:TextBox ID="fullNameTB" CssClass="CharacterCount" connectBtn="SignUpBtn" runat="server"></asp:TextBox>

                <br />

                <label for="emailTB" id="emailLabel">מייל</label>

                <asp:TextBox ID="emailTB" CssClass="CharacterCount" connectBtn="SignUpBtn" runat="server" type="email"></asp:TextBox>
                <br />
                <asp:Label ID="infoLabel" runat="server" Text="*לצורך זיהוי בלבד"></asp:Label>
                <br />


                <label for="departmentTB">מחלקה</label>

                <asp:DropDownList ID="departmentDD" AutoPostBack="False" runat="server"></asp:DropDownList>

                <asp:Button ID="SignUpBtn" CssClass="actionBtns" runat="server" Text="הירשם והתחל" OnClick="SignUpBtn_Click" disabled="true" />

                <asp:HyperLink ID="SignInLink" NavigateUrl="#" Text="שיחקת כבר בעבר? היכנס כאן." runat="server" />

                <asp:LinkButton id="profileBottomDiv" runat="server">
                    <asp:Image ID="LogOutImage" runat="server" ImageUrl="~/images/logout.svg" />
                    <asp:Label runat="server">לא הפרטים שלך? החלף משתמש</asp:Label>
                </asp:LinkButton>

            </div>





                <%-------------- דיב פנימי לכניסה למשתמשים קימות---------------%>

            <div id="emailSignInDiv" class="userInfoInput container">

                <div id="innerSignInDiv">
                     <h3 id="userExistsH3">משתמש קיים?</h3>
                    <h3 class="instructions">נא הזן כתובת מייל.</h3>


                    <asp:TextBox ID="emailTB1" runat="server" CssClass="CharacterCount" connectBtn="SignInBtn" type="email"></asp:TextBox>
        
                    <asp:Label ID="infoLabel1" runat="server" Text="*לצורך זיהוי בלבד"></asp:Label>

                </div>


                <asp:Button ID="SignInBtn" CssClass="actionBtns" runat="server" Text="התחבר והמשך" OnClick="SignInBtn_Click" disabled="true" />


                <asp:HyperLink ID="SignUpLink" NavigateUrl="#" Text="משתמש חדש? הירשם כאן." runat="server" />


            </div>






        </div>




        <div id="howtoplayContainer" class="container" containerid="1" runat="server">

            <label id="welcomeLabel" runat="server" />

            <h3>איך משחקים</h3>

            <ul>
                <li>החלק ימינה ושמאלה על מנת לעבור בין נתוני המטופל.​</li>
                <li>על מנת לסמן את הבדיקה כמתאימה: החלק את המסך ימינה או לחץ על כפתור הוי.​</li>
                <li>על מנת לסמן בדיקה כלא מתאימה: החלק את המסך שמאלה או לחץ על כפתור האיקס. ​</li>
            </ul>

            <asp:Button ID="navToGameBtn" CssClass="actionBtns" runat="server" Text="המשך למשחק" />

        </div>



        <%-------------- עמוד בחירת קטגוריה---------------%>


        <div id="preGameContainer" class="container" containerid="2" runat="server">

            <asp:Label id="catFinishedLabel" runat="server" />

            <asp:label id="chooseCatgLabel" runat="server">איזו בדיקה תרצה לתרגל?</asp:label>

            <asp:DropDownList ID="chooseCatDDL" runat="server" OnSelectedIndexChanged="chooseCat_OnSelectedIndexChanged">

            </asp:DropDownList>

            <asp:Button ID="startGameButton" CssClass="actionBtns" runat="server" Text="התחל משחק" OnClick="startGameButton_Click"/>

            <asp:HiddenField ID="NavHiddenField" runat="server" />
            <asp:HiddenField ID="AnswerHiddenField" runat="server" />
            <asp:HiddenField ID="SecondsHiddenField" runat="server" />
            <asp:HiddenField ID="UserLoggedInHiddenField" runat="server" />
            <asp:HiddenField ID="AnsweredCasesHiddenField" runat="server" />
            <asp:HiddenField ID="CasesCountHiddenField" runat="server" />



        </div>


        <%-------------- עמוד המשחק---------------%>

        <div id="gameContainer" class="container" containerid="2" runat="server">


            <div id="timerDiv">

                <asp:Label ID="CountDownLabel" runat="server" Text="30"></asp:Label>
                <asp:ImageButton ID="PauseImageButton" CssClass="icons" runat="server" ImageUrl="~/images/pause.svg" />

                <div id="starImagesDiv">
                    <asp:Image ID="StarIcon1" class="starIcons" runat="server" ImageUrl="~/images/star.svg" />
                    <asp:Image ID="StarIcon2" class="starIcons" runat="server" ImageUrl="~/images/star.svg" />
                    <asp:Image ID="StarIcon3" class="starIcons" runat="server" ImageUrl="~/images/star.svg" />
                </div>

            </div>



            <asp:LinkButton ID="ContinueLinkButton" runat="server" Text="לחץ על כפתור ההמשך או לחץ כאן כדי להמשיך לשחק." CauseValidation="false"></asp:LinkButton>

            <asp:ImageButton ID="CategorybackBtn" CssClass="icons" runat="server" ImageUrl="~/images/logout.svg" OnClick="CategorybackBtn_Click" />

            <div id="bgSwiperDiv" runat="server" ontouchstart="touchStart(event,'bgSwiperDiv');" ontouchend="touchEnd(event);" ontouchmove="touchMove(event);" ontouchcancel="touchCancel(event);"></div>


            <div id="categoryDiv">
                <h3 id="CategoryName" runat="server"></h3>
                <asp:Label ID="allCatLabel" runat="server"></asp:Label>
            </div>



            <!-- Swiper -->

            <div id="patiantDetailsDiv">

                <div id="mySwiper" class="swiper-container">

                    <div class="swiper-wrapper" id="swipperwrapperid" runat="server"></div>
                    <!-- Add Pagination -->
                    <div class="swiper-pagination"></div>

                </div>

            </div>


            <!-- Initialize Swiper -->

            <script>
                var swiper = new Swiper('.swiper-container', {
                    spaceBetween: 30,
                    pagination: {
                        el: '.swiper-pagination',
                        clickable: true,
                    },
                });
            </script>


            <asp:Panel ID="buttonsPanel" runat="server">

                <asp:Button ID="sendBtn" CssClass="gameBtns" runat="server" Text="חיוני" OnClick="GameBtns_Click" />
                <asp:Button ID="dontSendBtn" CssClass="gameBtns" runat="server" Text="לא חיוני" OnClick="GameBtns_Click" />
            </asp:Panel>


            <asp:Image ID="sendImg" runat="server" ImageUrl="~/images/approved.png" />
            <asp:Image ID="dontsendImg" runat="server" ImageUrl="~/images/dismissed.jpg" />

        </div>





        <%-------------- עמוד נגמר הזמן---------------%>

        <div id="TimeEndContainer" class="container" containerid="2" runat="server">

            <asp:Label ID="OutOfTimeLabel" runat="server" Text="נגמר הזמן"></asp:Label>

            <asp:Button ID="ContinueGame" CssClass="actionBtns" runat="server" Text="המשך משחק" OnClick="startGameButton_Click" />


        </div>



        <%-------------- עמוד משוב---------------%>

        <div id="feedbackContainer" class="container" containerid="2" runat="server">
            <asp:Label ID="feedbackTitle" runat="server"></asp:Label>

            <asp:Label ID="feedbackExp" runat="server"></asp:Label>

            <br />

            <asp:Label ID="TimeLabel" runat="server"></asp:Label>



            <asp:Panel id="departmentsDiv" runat="server">
                <h5>ניקוד לפי מחלקה</h5>
                <div id="testDiv" runat="server"></div>
            </asp:Panel>


            <asp:Button ID="nextqBtn" runat="server" Text="שאלה הבאה" OnClick="startGameButton_Click" />
        </div>




        <%-------------- עמוד תוצאות---------------%>

        <div id="scoreContainer" class="container" containerid="3" runat="server">

            <h5>איזור אישי</h5>

            <div id="topScoreBar" runat="server">

            <div id="avgSecondsDiv">
                <label id="avgSecP" runat="server"></label>
                <asp:Image ID="avgSecondsIcon" class="scoreIcons" runat="server" ImageUrl="~/images/hourglass.svg" />
            </div>

            <div id="userAnsweredDiv" runat="server">
            <p class="charts">מספר שאלות שנענו</p>
            <canvas id="myChart" class="charts"></canvas>
            </div>

            </div>

              <h5>הרופאים המובילים</h5>
            <div id="topDoctorsDiv" runat="server">
                <div id="docDiv3" class="docDivs" runat="server">
                    <asp:Image class="topDocsImage" runat="server" ImageUrl="~/images/user.svg" />
                    <asp:Label class="topDocsPlace" runat="server" Text="3"></asp:Label>
                    <asp:Label class="topDocLabel" runat="server" Text="דר הילה סטפאני פיטם"></asp:Label>
                </div>
                <div id="docDiv1" class="docDivs" runat="server">
                    <asp:Image class="topDocsImage" runat="server" ImageUrl="~/images/user.svg" />
                    <asp:Label class="topDocsPlace" runat="server" Text="1"></asp:Label>
                    <asp:Label class="topDocLabel" runat="server" Text="דר שירה שםארוףמאוד פיקר"></asp:Label>
                </div>
                <div id="docDiv2" class="docDivs" runat="server">
                    <asp:Image class="topDocsImage" runat="server" ImageUrl="~/images/user.svg" />
                    <asp:Label class="topDocsPlace" runat="server" Text="2"></asp:Label>
                    <asp:Label class="topDocLabel" runat="server" Text="שר דודי סימן - טוב"></asp:Label>
                </div>

            </div>

            <h5>ניקוד מחלקתי</h5>

            <div id="depScoresDiv" runat="server">
            </div>


            <%--                     <canvas id="myChart2"></canvas>--%>
        </div>

        <%-------------- עמוד עוד---------------%>


        <div id="moreContainer" class="container" containerid="4" runat="server">
            <h5 id="moreh5">אפשרויות נוספות</h5>
            <div id="moreBtnsDiv">
                 <asp:LinkButton ID="aboutBtn" class="moreBtns" runat="server">
                        <asp:Image ID="aboutImage" class="moreImage" runat="server" ImageUrl="~/images/about.PNG" />
                        <asp:Label ID="aboutLabel" class="moreLabel" runat="server" Text="אודות"></asp:Label>
                  </asp:LinkButton>
                  <asp:LinkButton ID="contantBtn" class="moreBtns" runat="server">
                        <asp:Image ID="contactImage" class="moreImage" runat="server" ImageUrl="~/images/contact.PNG" />
                        <asp:Label ID="contactLabel" class="moreLabel" runat="server" Text="צור קשר"></asp:Label>
                  </asp:LinkButton>
                  <asp:LinkButton ID="copyrightBtn" class="moreBtns" runat="server">
                        <asp:Image ID="copyrightImage" class="moreImage" runat="server" ImageUrl="~/images/copyright.PNG" />
                        <asp:Label ID="copyrightLabel" class="moreLabel" runat="server" Text="זכויות יוצרים"></asp:Label>
                  </asp:LinkButton>
                  <asp:LinkButton ID="editorBtn" class="moreBtns" runat="server">
                        <asp:Image ID="editorImage" class="moreImage" runat="server" ImageUrl="~/images/edit.PNG" />
                        <asp:Label ID="editorLabel" class="moreLabel" runat="server" Text="צד עורך (הנהלה)"></asp:Label>
                  </asp:LinkButton>
            </div>
        </div>




        <!----------תפריט הניווט ----------->
        <nav>
            <ul>
                <li>
                    <asp:LinkButton ID="profileLinkButton" runat="server" class="navBtn" containernum="0">
                        <asp:Image ID="proflieNavImage" class="navBtnImage" imageNum="0" runat="server" ImageUrl="~/images/user.svg" />
                        <asp:Label ID="profileLabel" class="navBtnLabel" runat="server" Text="פרופיל"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton runat="server" class="navBtn" containernum="1" disabled="true">
                        <asp:Image ID="howtoplayNavImage" class="navBtnImage" imageNum="1" runat="server" ImageUrl="~/images/problem-solving.svg" />
                        <asp:Label class="navBtnLabel" runat="server" Text="איך משחקים"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="gameNavBtn" runat="server" class="navBtn" containernum="2" disabled="true">
                            <asp:Image ID="gameNavImage" class="navBtnImage" imageNum="2" runat="server" ImageUrl="~/images/console.svg" />
                            <asp:Label class="navBtnLabel" runat="server" Text="משחק"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton runat="server" class="navBtn" containernum="3" OnClick="Results_Click" disabled="true">
                            <asp:Image ID="resultsNavImage" class="navBtnImage" imageNum="3" runat="server" ImageUrl="~/images/trophy.svg" />
                            <asp:Label class="navBtnLabel" runat="server" Text="תוצאות"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li>

                    <asp:LinkButton runat="server" class="navBtn" containernum="4" disabled="true">
                        <asp:Image ID="moreNavImage" class="navBtnImage" imageNum="4" runat="server" ImageUrl="~/images/more.svg" />
                        <asp:Label class="navBtnLabel" runat="server" Text="עוד"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </nav>


    </form>


</body>
</html>
