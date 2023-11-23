using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Lesson_14.Models.Auxiliary
{
    /// <summary>
    /// Всплывающее сообщение
    /// </summary>
    public class DisplayMessageClass : ViewModelBase
    {
        /// <summary>
        /// Отображение сообщения
        /// </summary>
        public string Visible
        {
            get
            {
                return visible;
            }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    RaisePropertyChanged(nameof(this.Visible));
                }
            }
        }
        private string visible = "Visible";

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    Visible = "Visible";
                    Timer();
                    text = value;
                    RaisePropertyChanged(nameof(this.Text));
                }
            }
        }
        private string text;

        /// <summary>
        /// Таймера
        /// </summary>
        private System.Timers.Timer aTimer;

        /// <summary>
        /// Включение таймера
        /// </summary>
        private void Timer()
        {
            aTimer = new System.Timers.Timer(2000);
            aTimer.Elapsed += MuteMessage;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        /// <summary>
        /// Отключение видимости уведомления
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void MuteMessage(Object source, ElapsedEventArgs e)
        {
            aTimer.Dispose();
            Visible = "Hidden";
            text = "";
        }
    }
}
