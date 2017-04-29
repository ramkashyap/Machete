// This file was automatically generated and may be regenerated at any
// time. To ensure any changes are retained, modify the tool with any segment/component/group/field name
// or type changes.
namespace Machete.HL7Schema.V26.Maps
{
    using V26;

    /// <summary>
    /// NMQ_N01 (MessageMap) - 
    /// </summary>
    public class NMQ_N01Map :
        HL7TemplateMap<NMQ_N01>
    {
        public NMQ_N01Map()
        {
            Segment(x => x.MSH, 0, x => x.Required = true);
            Segments(x => x.SFT, 1);
            Segment(x => x.UAC, 2);
            Group(x => x.QryWithDetail, 3);
            Groups(x => x.ClockAndStatistics, 4, x => x.Required = true);
        }
    }
}