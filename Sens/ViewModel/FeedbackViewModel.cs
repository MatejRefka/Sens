using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sens.ViewModel {
    public class FeedbackViewModel : BaseViewModel {

        //FIELDS
        private string comboBoxValue= "General Feedback";
        private bool submitVisibility;
        private string subject;
        private string body;
        private bool addLogFile;

        //PROPERTIES

        #region Populating combobox
        public List<String> FeedbackOptions { get; } = new List<string>
        {
            "General Feedback",
            "Feature Request",
            "Software Bug",
        };
        #endregion Populating combobox

        #region Form Properties
        public string ComboBoxValue {
            get {
                return comboBoxValue;
            }
            set {
                comboBoxValue = value;
                OnPropertyChanged("ComboBoxValue");
            }
        } 

        public string Subject {
            get {
                return subject;
            }
            set {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }

        public string Body {
            get {
                return body;
            }
            set {
                body = value;
                OnPropertyChanged("Body");
            }
        }

        public bool AddLogFile {
            get {
                return addLogFile;
            }
            set {
                addLogFile = value;
                OnPropertyChanged("AddLogFile");
            }
        }

        public bool SubmitVisibility {
            get {
                return submitVisibility;
            }
            set {
                submitVisibility = value;
                OnPropertyChanged("SubmitVisibility");
            }
        }
        #endregion Form Properties

        //COMMANDS
        #region Submit Command
        public ICommand SubmitCommand { get; set; }
        #endregion Submit Command

        //CONSTRUCTOR
        public FeedbackViewModel() {
            SubmitCommand = new RelayCommand(() => Submit());
        }

        //METHODS

        #region Submit Button Clicked
        private void Submit() {
            //send an email (feedback)
            SendFeedback();
            //control the visibility of 'Thank you' reply followed by fade out
            SubmitVisibility = true;
            SubmitVisibility = false;
        }
        #endregion Submit Button Clicked

        #region Send Email from the feedback form
        private void SendFeedback() {

            //To and From addresses
            var fromAddress = new MailAddress("sensitivityConfig@gmail.com", "Feedback");
            var toAddress = new MailAddress("matejrefka@googlemail.com");
            const string fromPassword = "fTwwWq2DV2r2dUL";

            //populating Subject and Body
            string subject = ComboBoxValue + ": " + Subject;
            string body = Body;

            //create a file attachment
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"DebugFile.txt";
            Attachment attachment = new Attachment(filePath, MediaTypeNames.Application.Octet);

            //client settings
            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            //populating the message and sending it
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body

            }) {
                if (AddLogFile == true) {
                    message.Attachments.Add(attachment);
                    client.Send(message);
                }
                else {
                    client.Send(message);
                }
            }
        }
        #endregion Send Email from the feedback form

    }

}

