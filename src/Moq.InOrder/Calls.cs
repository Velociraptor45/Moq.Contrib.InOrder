using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Moq.InOrder
{
    public class Calls : IEnumerable<Call>
    {
        private readonly IList<Call> _calls = new List<Call>();

        public string Expressions
        {
            get
            {
                var expressions = _calls.Select(x => x.Expression);
                return string.Join($",{Environment.NewLine}", expressions);
            }
        }

        public void Add(Call call)
        {
            _calls.Add(call);
        }

        public void RemoveFirst()
        {
            _calls.RemoveAt(0);
        }

        public IEnumerator<Call> GetEnumerator()
        {
            return _calls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}