// This file was automatically generated and may be regenerated at any
// time. To ensure any changes are retained, modify the tool with any segment/component/group/field name
// or type changes.
namespace Machete.HL7Schema.V26.Maps
{
    using V26;

    /// <summary>
    /// MFQ_M01 (MessageMap) - 
    /// </summary>
    public class MFQ_M01Map :
        HL7TemplateMap<MFQ_M01>
    {
        public MFQ_M01Map()
        {
            Segment(x => x.MSH, 0, x => x.Required = true);
            Segments(x => x.SFT, 1);
            Segment(x => x.UAC, 2);
            Segment(x => x.QRD, 3, x => x.Required = true);
            Segment(x => x.QRF, 4);
            Segment(x => x.DSC, 5);
        }
    }
}