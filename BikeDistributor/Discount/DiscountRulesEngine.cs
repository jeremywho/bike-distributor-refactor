using System.Collections.Generic;

namespace BikeDistributor.Discount
{
    public class DiscountRulesEngine<T>
    {
        private readonly List<Discount<T>> _rules;
        public DiscountRulesEngine(List<Discount<T>> rules)
        {
            _rules = rules;
        }

        public Discount<T> GetDiscount(T line)
        {
            foreach (var rule in _rules)
            {
                if (rule.Qualifies(line))
                {
                    return rule;
                }
            }

            return null;
        }
    }
}