using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeonHitchContentService.EntityModel.QueryResults;

namespace NeonHitchContentService.EntityModel
{
    public class NormalisedResult
    {
        public ContentType Type { get; set; }

        public string Artist { get; set; }

        public string Value { get; set; }

        public string Key { get; set; }

        public int Position { get; set; }

        // TODO: other stuff to help decide on how to sort content

        public Content Evaluate()
        {
            return new Content
            {
                Key = Key,
                Value = Value,
                Type = Type,
            };
        }
    }
}
