﻿using System;
using System.Collections.Generic;

namespace CLI.Observer
{
    public class SubjectNotifier
    {
        private List<IObserver> _observers;
        public SubjectNotifier()
        {
            _observers = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Ucitaj();
            }
        }
    }
}
