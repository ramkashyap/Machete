// This file was automatically generated and may be regenerated at any
// time. To ensure any changes are retained, modify the tool with any segment/component/group/field name
// or type changes.
namespace Machete.HL7Schema.V26.Maps
{
    using V26;

    /// <summary>
    /// RQI_I01_GUARANTOR_INSURANCE (GroupMap) - 
    /// </summary>
    public class RQI_I01_GUARANTOR_INSURANCEMap :
        HL7TemplateMap<RQI_I01_GUARANTOR_INSURANCE>
    {
        public RQI_I01_GUARANTOR_INSURANCEMap()
        {
            Segments(x => x.GT1, 0);
            Groups(x => x.Insurance, 1, x => x.Required = true);
        }
    }
}