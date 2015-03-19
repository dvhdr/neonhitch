﻿using System;
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

    public static class ResultHelpers
    {
        public static ContentResult Evaluate(this IEnumerable<NormalisedResult> normalisedResults)
        {
            var normalisedResultList = normalisedResults as IList<NormalisedResult> ?? normalisedResults.ToList();

            var skipCounter = 0;
            var positionCounter = 0;
            while (positionCounter < normalisedResultList.Count())
            {
                var image = normalisedResultList.Where(x => x.Type == ContentType.Image).Skip(skipCounter).FirstOrDefault();
                if (image != null)
                {
                    image.Position = positionCounter;
                    positionCounter++;
                }


                var text = normalisedResultList.Where(x => x.Type == ContentType.Text).Skip(skipCounter).FirstOrDefault();
                if (text != null)
                {
                    text.Position = positionCounter;
                    positionCounter++;
                }

                var video = normalisedResultList.Where(x => x.Type == ContentType.Video).Skip(skipCounter).FirstOrDefault();
                if (video != null)
                {
                    video.Position = positionCounter;
                    positionCounter++;
                }

                skipCounter++;
            }

            var orderedResults = normalisedResultList.OrderBy(x => x.Position);
            var contentItems = orderedResults.Select(normalisedResult => normalisedResult.Evaluate()).ToList();

            return new ContentResult
            {
                Results = contentItems.ToArray(),
                ResultCount = contentItems.Count,
            };
        } 
    }
}
