// This file was automatically generated and may be regenerated at any
// time. To ensure any changes are retained, modify the tool with any segment/component/group/field name
// or type changes.
namespace Machete.HL7Schema.V26.Maps
{
    using V26;

    /// <summary>
    /// RRD_O14_RESPONSE (GroupMap) - 
    /// </summary>
    public class RRD_O14_RESPONSEMap :
        HL7TemplateMap<RRD_O14_RESPONSE>
    {
        public RRD_O14_RESPONSEMap()
        {
            Group(x => x.Patient, 0);
            Groups(x => x.Order, 1, x => x.Required = true);
        }
    }
}