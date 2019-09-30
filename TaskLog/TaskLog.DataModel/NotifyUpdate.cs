/* Ceated by Ya Lin. 2019/9/23 15:11:54 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TaskLog.DataModel
{
    public class NotifyUpdate : INotifyPropertyChanging
    {
        public event PropertyChangingEventHandler PropertyChanging;

        public virtual void OnPropertyChanging(string propertyName)
        {
            var propertyChanging = PropertyChanging;
            if (propertyChanging != null)
            {
                propertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
    }
}
