using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsSystem.Controls.ErrorHandlerControl
{
    public enum MessageType
    {
        Success,
        Info,
        Warning,
        Error
    }

    public class NotificationMessage
    {
        public string Text { get; set; }
        public MessageType Type { get; set; }
        public bool AutoHide { get; set; }
    }

    public partial class ErrorHandlerControl : System.Web.UI.UserControl
    {

        private const string KEY_NOTIFICATION_MESSAGES = "NotificationMessages";
        private const string KEY_SHOW_AFTER_REDIRECT =
            "NotificationMessagesShowAfterRedirect";

        public static void AddMessage(NotificationMessage msg)
        {
            List<NotificationMessage> messages = NotificationMessages;
            if (messages == null)
            {
                messages = new List<NotificationMessage>();
            }
            messages.Add(msg);
            HttpContext.Current.Session[KEY_NOTIFICATION_MESSAGES] = messages;
        }

        public static bool ShowAfterRedirect
        {
            get
            {
                object showAfterRedirect =
                    HttpContext.Current.Session[KEY_SHOW_AFTER_REDIRECT];
                return (showAfterRedirect != null);
            }
            set
            {
                if (value)
                {
                    HttpContext.Current.Session[KEY_SHOW_AFTER_REDIRECT] = true;
                }
                else
                {
                    HttpContext.Current.Session.Remove(KEY_SHOW_AFTER_REDIRECT);
                }
            }
        }

        private static void ClearMessages()
        {
            HttpContext.Current.Session[KEY_NOTIFICATION_MESSAGES] = null;
        }

        private static List<NotificationMessage> NotificationMessages
        {
            get
            {
                List<NotificationMessage> messages = (List<NotificationMessage>)
                    HttpContext.Current.Session[KEY_NOTIFICATION_MESSAGES];
                return messages;
            }
        }

        public static void AddInfoMessage(string msg)
        {
            AddMessage(new NotificationMessage()
            {
                Text = msg,
                Type = MessageType.Info,
                AutoHide = true
            });
        }

        public static void AddSuccessMessage(string msg)
        {
            AddMessage(new NotificationMessage()
            {
                Text = msg,
                Type = MessageType.Success,
                AutoHide = true
            });
        }

        public static void AddWarningMessage(string msg)
        {
            AddMessage(new NotificationMessage()
            {
                Text = msg,
                Type = MessageType.Warning,
                AutoHide = false
            });
        }

        public static void AddErrorMessage(string msg)
        {
            AddMessage(new NotificationMessage()
            {
                Text = msg,
                Type = MessageType.Error,
                AutoHide = false
            });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowWaitingNotificationMessages();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!ShowAfterRedirect)
            {
                ShowWaitingNotificationMessages();
            }
        }

        private void ShowWaitingNotificationMessages()
        {
            if (NotificationMessages != null)
            {
                int index = 1;
                foreach (var msg in NotificationMessages)
                {
                    Panel msgPanel = new Panel();
                    msgPanel.CssClass = "PanelNotificationBox Panel" + msg.Type;
                    if (msg.AutoHide)
                    {
                        msgPanel.CssClass += " AutoHide";
                    }
                    msgPanel.ID = msg.Type + "Msg" + index;

                    Literal msgLiteral = new Literal();
                    msgLiteral.Mode = LiteralMode.Encode;
                    msgLiteral.Text = msg.Text;

                    msgPanel.Controls.Add(msgLiteral);

                    this.Controls.Add(msgPanel);
                    index++;
                }
                ClearMessages();

                IncludeTheCssAndJavaScript();
            }

            ShowAfterRedirect = false;
        }

        private void IncludeTheCssAndJavaScript()
        {
            ClientScriptManager cs = Page.ClientScript;

            // Include the jQuery library (if not already included)
            string jqueryURL = "/Controls/ErrorHandlerControl/Scripts/jquery-1.10.2.min.js";
            if (!cs.IsStartupScriptRegistered(jqueryURL))
            {
                cs.RegisterClientScriptInclude(jqueryURL, jqueryURL);
            }

            string notifierScriptURL = "/Controls/ErrorHandlerControl/Scripts/ErrorSuccessNotifier.js";
            if (!cs.IsStartupScriptRegistered(notifierScriptURL))
            {
                cs.RegisterClientScriptInclude(notifierScriptURL, notifierScriptURL);
            }

            // Include the ErrorSuccessNotifier.css stylesheet (if not already included)
            string cssRelativeURL = "/Controls/ErrorHandlerControl/Styles/ErrorSuccessNotifier.css";
            if (!cs.IsClientScriptBlockRegistered(cssRelativeURL))
            {
                string cssLinkCode = string.Format(
                    @"<link href='{0}' rel='stylesheet' type='text/css' />",
                    cssRelativeURL);
                cs.RegisterClientScriptBlock(this.GetType(), cssRelativeURL, cssLinkCode);
            }
        }
    }
}