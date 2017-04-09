using System;
using System.Collections.Generic;
using System.Linq;

namespace BikeDistributor.Discount
{
    public class Discount<T>
    {
        private List<Func<T, bool>> CompiledRules { get; }
        public IEnumerable<Rule> Rules { get; }
        public decimal Amount;
        public string Code { get; set; }

        public Discount(string code, decimal amount, IEnumerable<Rule> rules)
        {
            Code = code;
            Amount = amount;
            Rules = rules;            
            CompiledRules = Rules.Select(r => r.Compile<T>()).ToList();
        }

        public bool Qualifies(T item)
        {
            return CompiledRules.All(rule => rule(item));
        }
    }
}
