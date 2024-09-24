using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clear
{
    public class Dispose : MonoBehaviour
    {
        private List<IDisposable> _disposables = new();

        public void Add(IDisposable disposable) => _disposables.Add(disposable);
        public void Remove(IDisposable disposable) => _disposables.Remove(disposable);


        private void OnDisable()
        {
            _disposables.ForEach(dis => dis.Dispose());
        }
    }
}