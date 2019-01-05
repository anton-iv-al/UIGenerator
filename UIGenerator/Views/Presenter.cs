﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIGenerator.Views {
    public class Presenter<T> {
        protected readonly T _view;

        public Presenter(T view) {
            _view = view;
        }
    }
}