using System;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;
using System.Web.UI;
using Image = System.Drawing.Image;
using System.Collections.Generic;
using System.Linq;

public partial class _Default : System.Web.UI.Page
{

    XmlDocument myDoc = new XmlDocument();
    bool rnd = false;
    //   int selectedCtg;
    int ctgCounter = 0;
    //  int currentCaseID;
    int rndNum = 1;
    int[] UserSeconds;
    bool usersuccess; //משתנה השומר האם המשתמש הצליח או לא

    protected void Page_Load(object sender, EventArgs e)
    {

        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));
        

        if(Session["firstWebEntrance"] == null) // אם המשתמש נכנס לאתר בפעם הראשונה
        {

            //---------------------הדפסת שמות המחלקות אל דף הפרופיל----------------//

            XmlNodeList myNodes3 = myDoc.SelectNodes("//departments/department");    //שליפת שמות המחלקות הקיימות

            foreach (XmlNode myNode in myNodes3)
            {
                ListItem DepartmentItems = new ListItem(); //צור פריט חדש לרשימת דרופ דאוון
                DepartmentItems.Text = Server.UrlDecode(myNode.InnerXml);
                DepartmentItems.Value = Server.UrlDecode(myNode.Attributes["depID"].Value);
                departmentDD.Items.Add(DepartmentItems);
            }

            departmentDD.DataBind();

            Session["firstWebEntrance"] = "true";

            ViewState["selCategory"] = "0";

        }
        else
        {

            if(NavHiddenField.Value == "2" || NavHiddenField.Value == "GameFeedback")
            {

                catFinishedLabel.Text = "";


                //----------------הוצאת מספרי סיפורי המקרה שהמשתמש כבר ענה עליהם בצורה נכונה-----------------//

                XmlNodeList myCases = myDoc.SelectNodes("//users/user[@userid='" + (string)Session["selUserID"] + "']/caseID");

                List<int> ex = new List<int>(); //רשימת מספרי סיפורי המקרה שהמשתמש ענה עליהם או לא רלוונטים לקטגוריה שהמשתמש בחר

                foreach (XmlNode myNode in myCases)
                {
                    if (myNode != null) //אם יש סיפורי מקרה שהוא ענה עליהם בצורה נכונה
                    {
                        ex.Add(Convert.ToInt32(myNode.InnerXml));
                    }

                }


                //---------------------הדפסת שמות הבדיקות אל עמוד המשחק----------------//

                chooseCatDDL.Items.Clear();//איפוס הפריטים שבתוך כפתור הדרופ-דאוון

                ListItem listItem = new ListItem(); //צור פריט חדש לרשימת דרופ דאוון
                listItem.Text = ("כל הבדיקות");        //הוספת אופציה ראשונה להיבחן על כל הבדיקות
                listItem.Value = "0";
                chooseCatDDL.Items.Add(listItem);



                ////שליפת שמות הבדיקות הרפואיות הקיימות
                XmlNodeList myNodes2 = myDoc.SelectNodes("/game/medicalTests/mdTest");


                foreach (XmlNode myNode in myNodes2)
                {

                    int catNum = Convert.ToInt16(myNode.Attributes["mdTestId"].Value);

                    int caseCounter = 0;

                    XmlNodeList myNodes3 = myDoc.SelectNodes("/game//caseSudies/case[@mdTestId='" + catNum + "']");

                    foreach (XmlNode myNode1 in myNodes3)
                    {
                        if (ex.Contains(Convert.ToInt32(myNode1.Attributes["caseid"].Value)))
                        {

                        }
                        else
                        {
                            caseCounter++;
                        }
                    }

                    ListItem li = new ListItem(); //צור פריט חדש לרשימת דרופ דאוון
                    li.Text = Server.UrlDecode(myNode.InnerXml) + " (" + caseCounter.ToString() + ")";
                    if(caseCounter == 0)
                    {
                        li.Attributes.Add("disabled", "true");
                    }
                    li.Value = Server.UrlDecode(myNode.Attributes["mdTestId"].Value);
                    chooseCatDDL.Items.Add(li);
                }

                chooseCatDDL.DataBind();


            }


            



            //------------------- הדפסת גרף ניקוד לפי מחלקות לעמוד משוב ולעמוד תוצאות--------------//

            if (NavHiddenField.Value == "3" || NavHiddenField.Value == "GameFeedback") //במידה והמשתמש בעמוד תוצאות או משוב
            {

                XmlNodeList myNodes4 = myDoc.SelectNodes("//departments/department"); ////שליפת שמות המחלקות הקיימות

                double MaxNum = 0;

                foreach (XmlNode myNode in myNodes4)
                {
                    if (Convert.ToInt16(Server.UrlDecode(myNode.Attributes["depScore"].Value)) > MaxNum)
                    {
                        MaxNum = Convert.ToInt16(Server.UrlDecode(myNode.Attributes["depScore"].Value));
                    }
                }


                int a = 0;

                for (int i = Convert.ToInt16(MaxNum); i >= 0; i--)
                {
                    foreach (XmlNode myNode in myNodes4)
                    {

                        if (Convert.ToInt16(Server.UrlDecode(myNode.Attributes["depScore"].Value)) == i)
                        {
                            double currentScore = Convert.ToDouble(Server.UrlDecode(myNode.Attributes["depScore"].Value));

                            double WidthPercentage = (currentScore / MaxNum) * 95;

                            if (WidthPercentage < 26)
                            {
                                WidthPercentage = 26;
                            }


                            string[] colors = { "lightskyblue", "cornflowerblue", "mediumslateblue", "blueviolet", "darkslateblue" };

                            Label DepartmentScoreGraph = new Label(); //צור פריט חדש לרשימת דרופ דאוון
                            DepartmentScoreGraph.Text = Server.UrlDecode(myNode.InnerXml) + " (" + Server.UrlDecode(myNode.Attributes["depScore"].Value) + ")";
                            DepartmentScoreGraph.BackColor = Color.FromName(colors[a]);
                            DepartmentScoreGraph.CssClass = "depDiv";
                            DepartmentScoreGraph.Width = Unit.Percentage(WidthPercentage);

                            if (NavHiddenField.Value == "GameFeedback")
                            {
                                FindControl("departmentsDiv").Controls.Add(DepartmentScoreGraph);
                            }
                            if (NavHiddenField.Value == "3")
                            {
                                FindControl("depScoresDiv").Controls.Add(DepartmentScoreGraph);
                            }

                            a++;
                        }


                    }
                }

                //-------------------שמירת נתוני הסיפורי מקרה ונתוני המשתמש להצגת הגרפים בעמוד תוצאות---------------//

                //שליפת כמות הסיפורי מקרה שקיימים במערכת
                XmlNodeList myNodes5 = myDoc.SelectNodes("//caseSudies/case");

                int sumCases = myNodes5.Count;

                CasesCountHiddenField.Value = sumCases.ToString();

                //שליפת כמות הסיפורי מקרה שקיימים במערכת
                XmlNodeList CasesUserAnswered = myDoc.SelectNodes("//users/user[@userid='" + (string)Session["selUserID"] + "']/caseID");

                int CasesUserAns = CasesUserAnswered.Count;

                AnsweredCasesHiddenField.Value = CasesUserAns.ToString();

            }



        }

            //----------עדכון עמוד הפרופיל לאחר כניסה למערכת------------//

            if (UserLoggedInHiddenField.Value == "true" && NavHiddenField.Value == "0") //אם המשתמש נרשם/ נכנס למערכת בהצלחה
            {
                //הוספת תמונה לפרופיל האישי
                ImageButton profilePicBtn = new ImageButton();
                profilePicBtn.ID = "profilePicBtn";
                profilePicBtn.ImageUrl = "~/images/user.svg";

                //הוספת הוראות להוספת התמונה
                HyperLink addPic = new HyperLink();
                addPic.Text = "הוספת תמונת פרופיל";
                addPic.ID = "addPicHyperLink";

                //הוספת הקומפוננטות לעמוד
                FindControl("profileTopDiv").Controls.Add(profilePicBtn);
                FindControl("profileTopDiv").Controls.Add(addPic);

                if (Session["newUser"] != null) //אם משתמש ותיק נכנס למערכת עם מייל (מבלי להירשם כמשתמש חדש)
                {
                    //עדכון ומילוי תיבות הטקסט בדף פרופיל

                    fullNameTB.Text = myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']/username").InnerXml; 

                    emailTB.Text = myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']/useremail").InnerXml;

                    departmentDD.SelectedValue = Session["selUserDepartment"].ToString();
                }
            }
            else //אם המשתמש עוד לא נכנס למערכת בהצלחה
            {
                //יצירת לייבל להדפסת שם המשחק
                Label GameNameLabel = new Label();
                //GameNameLabel.ID = "caseLabel";
                GameNameLabel.Text = "<h2> משחק Smart-Med </h2>";
                FindControl("profileTopDiv").Controls.Add(GameNameLabel);
            }


    }

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void chooseCat_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        NavHiddenField.Value = "2";

    }


    protected void startGameButton_Click(object sender, EventArgs e)
    {

        ViewState["selCategory"] = chooseCatDDL.SelectedValue;

        //הגדרת ניווט אל עמוד המשחק
        NavHiddenField.Value = "GameStarted";

        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        //הגדרת משתנה עם מספר הקטגוריה שנבחרה
        int selectedCtg = Convert.ToInt32(ViewState["selCategory"]);

        //----------------הוצאת מספרי סיפורי המקרה שהמשתמש כבר ענה עליהם בצורה נכונה-----------------//

        XmlNodeList myCases = myDoc.SelectNodes("//users/user[@userid='" + (string)Session["selUserID"] + "']/caseID");

        List<int> ex = new List<int>(); //רשימת מספרי סיפורי המקרה שהמשתמש ענה עליהם או לא רלוונטים לקטגוריה שהמשתמש בחר

        foreach (XmlNode myNode in myCases)
        {
            if (myNode != null) //אם יש סיפורי מקרה שהוא ענה עליהם בצורה נכונה
            {
                ex.Add(Convert.ToInt32(myNode.InnerXml));
            }
        }



        //-------------------מציאת מספרי סיפורי המקרה הרלוונטים למשתמש והכנסת לרשימה-----------------------//

        List<int> caseIdOptions = new List<int>(); //רשימת מספרי סיפורי המקרה שרלוונטים

        bool IdOkay = true;

        if (selectedCtg != 0) //אם המשתמש בחר קטגוריה ספציפית לתרגל
        {
            XmlNodeList casesByCategory = myDoc.SelectNodes("//caseSudies/case[@mdTestId='" + selectedCtg + "']");

            foreach (XmlNode myNode in casesByCategory)
            {

                if (ex.Contains(Convert.ToInt32(myNode.Attributes["caseid"].Value)))
                {
                    IdOkay = false;
                }

                if (IdOkay == true)
                {
                    caseIdOptions.Add(Convert.ToInt32(myNode.Attributes["caseid"].Value)); //הוספת מספר מקרה

                }
            }

            allCatLabel.Text = "";
        }
        else //אם המשתמש בחר בחר לתרגל סיפורי מקרה מכל הקטגוריות
        {
            XmlNodeList allCases = myDoc.SelectNodes("//caseSudies/case");
            foreach (XmlNode myNode in allCases)
            {
                if (ex.Contains(Convert.ToInt32(myNode.Attributes["caseid"].Value)))
                {
                    IdOkay = false;
                }
                else
                {
                    IdOkay = true;
                }

                if (IdOkay == true)
                {
                    caseIdOptions.Add(Convert.ToInt32(myNode.Attributes["caseid"].Value)); //הוספת מספר מקרה
                }
            }

            allCatLabel.Text = "מתוך מאגר: כל הבדיקות";
        }


        if (caseIdOptions.Count < 1)
        {
            catFinishedLabel.Text = "נגמרו סיפורי המקרה בקטגוריה זו.";

            NavHiddenField.Value = "2";

        }
        else
        {

        Random objRand = new Random();        // Class for getting the random number
        int rndID = objRand.Next(caseIdOptions.Count); //הגרלה ועדכון מספר של המקרה מטופל שנבחר

        Session["selCaseID"] = (caseIdOptions[rndID]).ToString();

       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('currentCaseID:" + Session["selCaseID"] + "')", true);

        Console.WriteLine("Session['selCaseID']: " + Session["selCaseID"]);
        //הוצאת מספר הקטוריה של סיפור המקרה והדפסה אל תוך המשחק
        XmlNode getCatID = myDoc.SelectSingleNode("//caseSudies/case[@caseid='" + Session["selCaseID"] + "']");
        string currentCat = getCatID.Attributes["mdTestId"].Value;

        XmlNode myTestName = myDoc.SelectSingleNode("//medicalTests/mdTest[@mdTestId='" + currentCat + "']"); //הדפסת שם הקטגוריה
        CategoryName.InnerHtml = Server.UrlDecode(myTestName.InnerXml);



        //--------------------------הדפסת תוכן סיפור המקרה אל תוך המשחק------------------------//

        XmlNodeList myNodes = myDoc.SelectNodes("//caseSudies//case[@caseid='" + Session["selCaseID"] + "']/patientDetails");

        int a = 1;

        foreach (XmlNode myNode in myNodes)  //שליפת תוכן פרטי המטופל
        {

            var title = Server.UrlDecode(myDoc.SelectSingleNode("//caseSudies//case[@caseid='" + Session["selCaseID"] + "']/patientDetails/title").InnerXml);
            var content = Server.UrlDecode(myDoc.SelectSingleNode("//caseSudies//case[@caseid='" + Session["selCaseID"] + "']/patientDetails/content").InnerXml);

            //יצירת לייבל להדפסת כותרת פרטי המטופל
            Label caseLabel = new Label();
            //caseLabel.ID = "caseLabel";
            caseLabel.Text = "<h3>" + title + "</h3>" + "<p>" + content + "</p>";


            //יצירת פאנל חדש להכנסת תוכן פרטי המטופל
            Panel casePanel = new Panel();
            casePanel.CssClass = "swiper-slide";

            //הוספת הלייבלים אל תוך הפאנל והטמעתם בדף
            casePanel.Controls.Add(caseLabel);
            FindControl("swipperwrapperid").Controls.Add(casePanel);
            FindControl("swipperwrapperid").DataBind();

            a++;

        }


        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('currentCaseID:" + Session["selCaseID"] + "')", true);


        //הכנסת תשובה למקרה אל תוך hidden field
        XmlNode myAnswer = myDoc.SelectSingleNode("/game//case[@caseid='" + Session["selCaseID"] + "']");
        AnswerHiddenField.Value = myAnswer.Attributes["answer"].Value;

        }

    }



    protected void GameBtns_Click(object sender, EventArgs e)
    {
        //משתנה לזיהוי הכפתור משחק שהפעיל את הפונקציה
        Button button = (Button)sender;
        string buttonId = button.ID;

        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));


        //שליפת תשובה לפי המקרה סיפור
        XmlNode myCase = myDoc.SelectSingleNode("/game//case[@caseid='" + Session["selCaseID"] + "']");
        string answer = myCase.Attributes["answer"].Value;


        //הדפסה האם המשתמש טעה או צדק
        if (answer == "true")
        {
            if (buttonId == "dontSendBtn") //אם המשתמש לחץ על כפתור ה"לא חיוני"
            {
                usersuccess = false;
            }
            else//אם המשתמש לחץ על כפתור ה"חיוני"
            {
                usersuccess = true;
            }

        }
        if (answer == "false")
        {
            if (buttonId == "dontSendBtn") //אם המשתמש לחץ על כפתור ה"לא חיוני"
            {
                usersuccess = true;
            }
            else//אם המשתמש לחץ על כפתור ה"חיוני"
            {
                usersuccess = false;
            }
        }

        if (usersuccess == true)//אם המשתמש הצליח
        {
            feedbackTitle.Text = "<h4> תשובה נכונה! </h4>"; //הדפסת משוב 

            //שליפת ניקוד המחלקה 
            XmlNode depScore = myDoc.SelectSingleNode("//departments/department[@depID = '" + (string)Session["selUserDepartment"] + "']");

            //משתנה ששומר את הניקוד לאחר כל שאלה
            int currentPoints;

            //חישוב נקודות למחלקה בהתאם למספר שניות מענה על שאלה
            if (Convert.ToInt16(SecondsHiddenField.Value) < 11)
            {
                currentPoints = 3; //עדכון מספר הניקוד
            }
            else if (11 <= Convert.ToInt16(SecondsHiddenField.Value) && Convert.ToInt16(SecondsHiddenField.Value) < 21)
            {
                currentPoints = 2; //עדכון מספר הניקוד
            }
            else
            {
                currentPoints = 1; //עדכון מספר הניקוד
            }

            //עדכון ניקוד המחלקה 
            depScore.Attributes["depScore"].Value = (Convert.ToInt16(depScore.Attributes["depScore"].Value) + 1).ToString();


            //יצירת תגית עם מספר הסיפור מקרה עליו ענה המשתמש ומספר השניות שלקח לו לענות
            XmlElement caseIDNode = myDoc.CreateElement("caseID");
            caseIDNode.InnerXml = (Session["selCaseID"]).ToString(); //הכנסת ערך של מספר הסיפור מקרה
            caseIDNode.SetAttribute("seconds", SecondsHiddenField.Value);

            //הוספת הפריט לעץ
            myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']").AppendChild(caseIDNode);


            //שמירה ךעץ
            myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

            TimeLabel.Text = "ענית על השאלה תוך " + SecondsHiddenField.Value + " שניות, והוספת למחלקתך " + currentPoints + " נקודות.";

        }
        else//אם המשתמש לא הצליח
        {
            feedbackTitle.Text = "<h4> תשובה לא נכונה </h4>";
            TimeLabel.Text = "";

        }

        //הדפסת הסבר התשובה למשתמש
        XmlNode answerExplanation = myDoc.SelectSingleNode("/game//case[@caseid='" + Session["selCaseID"] + "']/answerExp");
        string answerExp = answerExplanation.InnerXml;
        feedbackExp.Text = answerExp;


    }

    protected void SignUpBtn_Click(object sender, EventArgs e)
    {
        bool newUser = true;
        ////חיפוש משתמש לפי המייל שהוזן
        XmlNodeList myNodes = myDoc.SelectNodes("//useremail");

        foreach (XmlNode myNode in myNodes)
        {
            if (emailTB.Text == myNode.InnerXml)
            {
                newUser = false;
                break;
            }
        }

        if(newUser == false)
        {
            infoLabel.Text = "מייל זה כבר רשום במערכת."; //עדכון הודעה למשתמש
            infoLabel.CssClass = "red";

            emailTB.Text = "";//איפוס המייל שהמשתמש הזין

            SignInLink.CssClass = "bounce-4"; //הוספת אנימציה ללינק למשתמשים רשומים

            NavHiddenField.Value = "";
        }
        else
        {
            //שליפת מספר מחלקה שנבחרה
            Session["selUserDepartment"] = departmentDD.SelectedValue;
            Session["selUserID"] = myDoc.SelectSingleNode("//users/usercounter").InnerXml;


            //שליפת מספר המשתמשים 
            int userCounter = Convert.ToInt16(myDoc.SelectSingleNode("//users/usercounter").InnerXml);

            //  יצירת משתמש חדש
            XmlElement NewUserNode = myDoc.CreateElement("user");
            //   myItemNode.InnerXml = Server.UrlEncode(imagesLibPath + imageNewName); //הוספת שם ופרטי התמונה
            NewUserNode.SetAttribute("userid", userCounter.ToString()); //הוספת מספר המשתמש
            NewUserNode.SetAttribute("userDepartment", (string)Session["selUserDepartment"]); //הוספת מספר מחלקה

            //יצירת תגית חדשה לשם מלא של המשתמש
            XmlElement UserNameNode = myDoc.CreateElement("username");
            UserNameNode.InnerXml = Server.UrlEncode(fullNameTB.Text); //הוספת שם מלא של המשתמש
            NewUserNode.AppendChild(UserNameNode);

            //יצירת תגית חדשה למייל של המשתמש
            XmlElement UserEmailNode = myDoc.CreateElement("useremail");
            UserEmailNode.InnerXml = Server.UrlEncode(emailTB.Text); //הוספת שם מייל של המשתמש
            NewUserNode.AppendChild(UserEmailNode);


            //הוספת הפריט לעץ
            myDoc.SelectSingleNode("//users").AppendChild(NewUserNode);

            //עדכון מספר המשתמשים
            userCounter++;
            myDoc.SelectSingleNode("//users/usercounter").InnerXml = userCounter.ToString();

            //שמירה של הפריטים החדשים בעץ
            myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

            welcomeLabel.InnerHtml = "שלום ד\"ר " + Server.UrlDecode(myDoc.SelectSingleNode("//user[@userid='" + Session["selUserID"] + "']").FirstChild.InnerXml);

            UserLoggedInHiddenField.Value = "true";

            NavHiddenField.Value = "1";

        }

          
 
    }

    protected void SignInBtn_Click(object sender, EventArgs e)
    {

        bool oldUser = false;

        ////חיפוש משתמש לפי המייל שהוזן
        XmlNodeList myNodes = myDoc.SelectNodes("//useremail");

        foreach (XmlNode myNode in myNodes)
        {
            if (emailTB1.Text == myNode.InnerXml)
            {
                oldUser = true;
            }
        }

        if(oldUser == true) //אם המייל קיים במערכת
        {

            ////חיפוש משתמש לפי המייל שהוזן
            XmlNodeList myNodes1 = myDoc.SelectNodes("//useremail");


            foreach (XmlNode myNode in myNodes1)
            {
                if (myNode.InnerXml == Server.UrlDecode(emailTB1.Text))
                {
                    Session["selUserID"] = myNode.ParentNode.Attributes["userid"].Value;
                    Session["selUserDepartment"] = myNode.ParentNode.Attributes["userDepartment"].Value;

                }
            }


            welcomeLabel.InnerHtml = "שלום ד\"ר " + myDoc.SelectSingleNode("//user[@userid='" + Session["selUserID"] + "']").FirstChild.InnerXml;

            NavHiddenField.Value = "1";

            UserLoggedInHiddenField.Value = "true"; //משתנה שמעדכן כי המשתמש הצליח לבצע הרשמה/ כניסה למערכת

            Session["newUser"] = "false"; //משתנה שמעדכן שהמשתמש ותיק ונכנס למערכת דרך המייל 

        }
        else //אם המייל לא נמצא במערכת
        {

            infoLabel1.Text = "מייל זה לא נמצא במערכת."; //עדכון הודעה למשתמש
            infoLabel1.CssClass = "red";

            emailTB1.Text = "";//איפוס המייל שהמשתמש הזין

            SignUpLink.CssClass = "bounce-4"; //הוספת אנימציה ללינק למשתמשים רשומים

            NavHiddenField.Value = "01";
        }
    }

    protected void CategorybackBtn_Click(object sender, ImageClickEventArgs e)
    {
        NavHiddenField.Value = "2";

       // catFinishedLabel.Text = "";
    }

    protected void Results_Click(object sender, EventArgs e)
    {
        NavHiddenField.Value = "3";

        //הטענת העץ
        //myDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        List<int> secondsList = new List<int>(); //מערך ששומר את מספר השניות למענה של המשתמש

        XmlNodeList casesAswered = myDoc.SelectNodes("//users/user[@userid='" + (string)Session["selUserID"] + "']/caseID");

        foreach (XmlNode myNode in casesAswered)
        {

            if (myNode != null) //אם יש סיפורי מקרה שהמשתמש ענה עליהם
            {
                secondsList.Add(Convert.ToInt32(myNode.Attributes["seconds"].Value));
            }

        }

        int myAvg; //משתנה השומר את הערך של ממוצע המשתמש

        if (secondsList.Count != 0) //אם יש סיפורי מקרה שהמשתמש ענה עליהם
        {
            myAvg = (secondsList.Sum() / secondsList.Count);
        }
        else
        {
            myAvg = 0;
        }

        avgSecP.InnerHtml = "זמן ממוצע למענה: </br>" + myAvg.ToString() + " שניות";


    }
}

