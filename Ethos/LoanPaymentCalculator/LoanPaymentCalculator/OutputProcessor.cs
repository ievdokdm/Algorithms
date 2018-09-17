using System;
using Newtonsoft.Json;

namespace LoanPaymentCalculator {
    public class OutputProcessor {
        private readonly Formatting formatting;

        public OutputProcessor(Formatting formatting) {
            this.formatting = formatting;
        }

        public OutputProcessor():this(Formatting.Indented)
        {}

        public string Process(Object obj){
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return JsonConvert.SerializeObject(obj, formatting);
        }
    }
}
