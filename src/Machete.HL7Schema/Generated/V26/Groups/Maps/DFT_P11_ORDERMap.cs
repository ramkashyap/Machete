// This file was automatically generated and may be regenerated at any
// time. To ensure any changes are retained, modify the tool with any segment/component/group/field name
// or type changes.
namespace Machete.HL7Schema.V26.Maps
{
    using V26;

    /// <summary>
    /// DFT_P11_ORDER (GroupMap) - 
    /// </summary>
    public class DFT_P11_ORDERMap :
        HL7TemplateMap<DFT_P11_ORDER>
    {
        public DFT_P11_ORDERMap()
        {
            Segment(x => x.OBR, 0, x => x.Required = true);
            Segments(x => x.NTE, 1);
        }
    }
}