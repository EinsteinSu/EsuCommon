﻿#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

#endregion

namespace Supeng.Common.Entities.ObserveCollection
{
    public class AsyncObservableCollection<T> : ObservableCollection<T>
    {
        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current;

        public AsyncObservableCollection()
        {
        }

        public AsyncObservableCollection(IEnumerable<T> list)
            : base(list)
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (SynchronizationContext.Current == synchronizationContext)
            {
                RaiseCollectionChanged(e);
            }
            else
            {
                synchronizationContext.Post(RaiseCollectionChanged, e);
            }
        }

        private void RaiseCollectionChanged(object param)
        {
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (SynchronizationContext.Current == synchronizationContext)
            {
                RaisePropertyChanged(e);
            }
            else
            {
                // Post the PropertyChanged event on the creator thread             
                synchronizationContext.Post(RaisePropertyChanged, e);
            }
        }

        private void RaisePropertyChanged(object param)
        {
            // We are in the creator thread, call the base implementation directly         
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }
    }
}