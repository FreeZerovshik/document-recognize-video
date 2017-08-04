using System;
using se.smartid;

namespace Recognition
{ 
    public partial class CsReporter : ResultReporterInterface
    {
        public CsReporter()
            : base()
        {
        }
        public override void SnapshotRejected()
        {
            Console.WriteLine("[Optional callback called]: Snapshot rejected");
        }

        public override void DocumentMatched(MatchResultVector match_results)
        {
            Console.WriteLine("[Optional callback called]: Document matched");
        }

        public override void DocumentSegmented(SegmentationResultVector segmentation_results)
        {
            Console.WriteLine("[Optional callback called]: Document segmented");
        }

        public override void SnapshotProcessed(RecognitionResult recog_result)
        {
            Console.WriteLine("[Optional callback called]: Snapshot processed");
        }

        public override void SessionEnded()
        {
            Console.WriteLine("[Optional callback called]: Session ended");
        }
    }
}
