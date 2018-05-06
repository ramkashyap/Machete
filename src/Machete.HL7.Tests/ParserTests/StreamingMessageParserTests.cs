﻿namespace Machete.HL7.Tests.ParserTests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using HL7Schema.V26;
    using NUnit.Framework;
    using Testing;
    using Texts;


    [TestFixture]
    public class StreamingMessageParserTests :
        HL7MacheteTestHarness<MSH, HL7Entity>
    {
        [Test]
        public async Task Should_be_able_to_parse_file_with_multiple_messages()
        {
            const string message = @"FHS|^~\&|XYZ|MACHETELAB|MACHETE HEALTH|MACHETE HEALTH|2016011209225417|||TEST|X1601982849541701
BHS|^~\&|XYZ|MACHETELAB|MACHETE HEALTH|MACHETE HEALTH|2016011209225417||||B000001
MSH|^~\&|XYZ|MACHETELAB|MACHETE HEALTH|MACHETE HEALTH|20160112092254||ORU^R01|M16012000000000001|T|2.3|||ER|ER
PID|1|7548547857847|7548547857847||TEST^MACHETELAB||19731129|F|||562 ASHBRIDGE DR, APT K^^OAKLAND^CA^94123|||||||
PV1|1|O||||||1134559404^CARTER^JOE^^^^^^^^^^NPI|||||||||||||||||||||||||||||||RN
ORC||515624304270|515624304270||||||20150605|||1134559404^CARTER^PUJEETA^^^^^^^^^^NPI
OBR|1||515624304270|028142^CBC, PLATELET, NO DIFFERENTIAL^L|||20150605|20150605||||||||1134559404^CARTER^JOE^^^^^^^^^^NPI||RN||19528512^MGHMD^6706085-02|||||F
OBX|1|NM|6690-2^LOINC^LN^005025^WBC||6|X10E3/UL|3.4-10.8||||F|||20150605|RN   ^^L
OBX|2|NM|789-8^LOINC^LN^005033^RBC||3.96|X10E6/UL|3.77-5.28||||F|||20150605|RN   ^^L
OBX|3|NM|718-7^LOINC^LN^005041^HEMOGLOBIN||11.8|G/DL|11.1-15.9||||F|||20150605|RN   ^^L
OBX|4|NM|4544-3^LOINC^LN^005058^HEMATOCRIT||35.8|%|34.0-46.6||||F|||20150605|RN   ^^L
OBX|5|NM|787-2^LOINC^LN^015065^MCV||90|FL|79-97||||F|||20150605|RN   ^^L
OBX|6|NM|785-6^LOINC^LN^015073^MCH||29.8|PG|26.6-33.0||||F|||20150605|RN   ^^L
OBX|7|NM|786-4^LOINC^LN^015081^MCHC||33|G/DL|31.5-35.7||||F|||20150605|RN   ^^L
OBX|8|NM|788-0^LOINC^LN^105007^RDW||13.7|%|12.3-15.4||||F|||20150605|RN   ^^L
OBX|9|NM|777-3^LOINC^LN^015172^PLATELETS||269|X10E3/UL|150-379||||F|||20150605|RN   ^^L
OBR|2||515624304270|322758^BASIC METABOLIC PANEL (8)^L|||20150605|20150605||||||||1134559404^CARTER^JOE^^^^^^^^^^NPI||RN||19528512^MGHMD^6706085-02|||||F
OBX|1|NM|2345-7^LOINC^LN^001032^GLUCOSE, SERUM||77|MG/DL|65-99||||F|||20150605|RN   ^^L
OBX|2|NM|3094-0^LOINC^LN^001040^BUN||11|MG/DL|6-24||||F|||20150605|RN   ^^L
OBX|3|NM|2160-0^LOINC^LN^001370^CREATININE, SERUM||.7|MG/DL|0.57-1.00||||F|||20150605|RN   ^^L
OBX|4|NM|48642-3^LOINC^LN^100791^EGFR IF NONAFRICN AM||108|ML/MIN/1.73|>59||||F|||20150605|^^L
OBX|5|NM|48643-1^LOINC^LN^100797^EGFR IF AFRICN AM||124|ML/MIN/1.73|>59||||F|||20150605|^^L
OBX|6|NM|3097-3^LOINC^LN^011577^BUN/CREATININE RATIO||16||9-23||||F|||20150605|^^L
OBX|7|NM|2951-2^LOINC^LN^001198^SODIUM, SERUM||139|MMOL/L|134-144||||F|||20150605|RN   ^^L
OBX|8|NM|2823-3^LOINC^LN^001180^POTASSIUM, SERUM||4.2|MMOL/L|3.5-5.2||||F|||20150605|RN   ^^L
OBX|9|NM|2075-0^LOINC^LN^001206^CHLORIDE, SERUM||101|MMOL/L|97-108||||F|||20150605|RN   ^^L
OBX|10|NM|2028-9^LOINC^LN^001578^CARBON DIOXIDE, TOTAL||26|MMOL/L|18-29||||F|||20150605|RN   ^^L
OBX|11|NM|17861-6^LOINC^LN^001016^CALCIUM, SERUM||8.8|MG/DL|8.7-10.2||||F|||20150605|RN   ^^L
OBR|3||515624304270|001453^HEMOGLOBIN A1C^L|||20150605|20150605||||||||1134559404^CARTER^JOE^^^^^^^^^^NPI||RN||19528512^MGHMD^6706085-02|||||F
OBX|1|NM|4548-4^LOINC^LN^001464^HEMOGLOBIN A1C||5.9|%|4.8-5.6|H|||F|||20150605|RN   ^^L
MSH|^~\&|XYZ|MACHETELAB|MACHETE HEALTH|MACHETE HEALTH|20160112092254||ORU^R01|M16012000000000002|T|2.3|||ER|ER
PID|1|834787483724|834787483724||TEST1^LABCORP1||19750104|M|||0162 E BALTIMORE ST^^BALTIMORE^MD^21224|||||||
PV1|1|O||||||1649421041^MUGANLINSKAYA^NARGIZ^^^^^^^^^^NPI|||||||||||||||||||||||||||||||RN
ORC||520424318740|520424318740||||||20150723|||1649421041^MUGANLINSKAYA^NARGIZ^^^^^^^^^^NPI
OBR|1||520424318740|550475^HCV GENOTYPING NON REFLEX^L|||20150723|20150723||||||||1649421041^MUGANLINSKAYA^NARGIZ^^^^^^^^^^NPI||RN||19528512^HELIB^91356570701|||||F
OBX|1|ST|32286-7^LOINC^LN^550511^HEPATITIS C GENOTYPE||HC1A||||||F|||20150723|BN   ^^L
NTE|1|L|1A
MSH|^~\&|XYZ|MACHETELAB|MACHETE HEALTH|MACHETE HEALTH|20160112092254||ORU^R01|M16012000000000003|T|2.3|||ER|ER
PID|1|AH73FEYGF7F7337673|AH73FEYGF7F7337673||TEST2^LABCORP2||19740730|F|||873823782 WUARRY PL^^WATERFORD^CT^06385||||||
PV1|1|O||||||1477550036^NULSEN^JOHN^^^^^^^^^^NPI|||||||||||||||||||||||||||||||LA
ORC||522546412110|522546412110||||||20150812|||1477550036^NULSEN^JOHN^^^^^^^^^^NPI
OBR|1||522546412110|139061^FDA GUIDANCE FEMALE WITH RFLX^L|||20150812|20150812||||||||1477550036^DOE^JOHN^^^^^^^^^^NPI||LALCA||06001190^BSCTA|||||F
OBX|1|ST|47364-5^LOINC^LN^138748^HEPATITIS B SURFACE ANTIGEN||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH BIO-RAD GENETIC SYSTEMS HEPATITIS B SURFACE
NTE|3|L|ANTIGEN KIT VERSION 3.0.
NTE|4|L|.
OBX|2|ST|47358-7^LOINC^LN^138698^HEPATITIS B CORE TOTAL AB||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ORTHO HEPATITIS B CORE TOTAL ANTIBODY KIT.
NTE|3|L|.
OBX|3|ST|47441-1^LOINC^LN^138908^HEPATITIS C VIRUS ANTIBODY||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ORTHO HEPATITIS C VIRUS ANTIBODY KIT VERSION 3.0.
NTE|3|L|.
OBX|4|ST|47361-1^LOINC^LN^139281^DONOR SYPHILIS(T PALLIDUM IGG)||NR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|TEST PERFORMED WITH TRINITY BIOTECH CAPTIA SYPHILIS
NTE|3|L|(T. PALLIDUM)-G KIT.
OBX|5|ST|59052-1^LOINC^LN^139241^HIV 1/HCV/HBV NAT||ULTNR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NONREACTIVE FOR HIV-1 RNA
NTE|2|L|NONREACTIVE FOR HCV RNA
NTE|3|L|NONREACTIVE FOR HBV DNA
NTE|4|L|TEST PERFORMED WITH NOVARTIS PROCLEIX ULTRIO ASSAY KIT.
OBX|6|ST|50411-8^LOINC^LN^138829^CHLAMYDIA, NUCLEIC ACID AMP||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
OBX|7|ST|50412-6^LOINC^LN^138830^GONOCOCCUS, NUCLEIC ACID AMP||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH GEN-PROBE APTIMA COMBO 2 KIT.
OBR|2||522546412110|138896^DONOR WNV (NAT) ASSAY^L|||20150812|20150812||||||||1477550036^DOE^JOHN^^^^^^^^^^NPI||LALCA||06001190^BSCTA|||||F
OBX|1|ST|34892-0^LOINC^LN^138899^PROCLEIX WNV||NR||NONREACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|NONREACTIVE FOR WNV RNA
NTE|3|L|TEST PERFORMED WITH PROCLEIX WNV KIT.
NTE|4|L|.
MSH|^~\&|XYZ|MACHETELAB|MACHETE HEALTH|MACHETE HEALTH|20160112092254||ORU^R01|M16012000000000004|T|2.3|||ER|ER
PID|1|G3746376437TRETR|G3746376437TRETR||TEST3^LABCORP3||19750720|M|||93493984 VICTORY RD^^WATERFORD^CT^06385||||||
PV1|1|O|||||||||||||||||||||||||||||||||||||LA
ORC||522546412150|522546412150||||||20150812
OBR|1||522546412150|139063^FDA GUIDANCE MALE WITH REFLEX^L|||20150812|20150812||||||||||LALCA||06001190^BSCTA|||||F
OBX|1|ST|47364-5^LOINC^LN^138748^HEPATITIS B SURFACE ANTIGEN||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH BIO-RAD GENETIC SYSTEMS HEPATITIS B SURFACE
NTE|3|L|ANTIGEN KIT VERSION 3.0.
NTE|4|L|.
OBX|2|ST|47358-7^LOINC^LN^138698^HEPATITIS B CORE TOTAL AB||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ORTHO HEPATITIS B CORE TOTAL ANTIBODY KIT.
NTE|3|L|.
OBX|3|ST|47441-1^LOINC^LN^138908^HEPATITIS C VIRUS ANTIBODY||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ORTHO HEPATITIS C VIRUS ANTIBODY KIT VERSION 3.0.
NTE|3|L|.
OBX|4|ST|47361-1^LOINC^LN^139281^DONOR SYPHILIS(T PALLIDUM IGG)||NR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|TEST PERFORMED WITH TRINITY BIOTECH CAPTIA SYPHILIS
NTE|3|L|(T. PALLIDUM)-G KIT.
OBX|5|ST|59052-1^LOINC^LN^139241^HIV 1/HCV/HBV NAT||ULTNR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NONREACTIVE FOR HIV-1 RNA
NTE|2|L|NONREACTIVE FOR HCV RNA
NTE|3|L|NONREACTIVE FOR HBV DNA
NTE|4|L|TEST PERFORMED WITH NOVARTIS PROCLEIX ULTRIO ASSAY KIT.
OBX|6|ST|44538-7^LOINC^LN^138781^HTLV-I/II ANTIBODIES, QUAL||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ABBOTT PRISM HTLV-I/HTLV-II ASSAY.
OBX|7|ST|47430-4^LOINC^LN^138663^CYTOMEGALOVIRUS CMV TOTAL AB||NR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|TEST PERFORMED WITH IMMUCOR CAPTURE-CMV IGG AND IGM KIT.
NTE|3|L|.
OBX|8|ST|50411-8^LOINC^LN^138829^CHLAMYDIA, NUCLEIC ACID AMP||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
OBX|9|ST|50412-6^LOINC^LN^138830^GONOCOCCUS, NUCLEIC ACID AMP||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH GEN-PROBE APTIMA COMBO 2 KIT.
OBR|2||522546412150|138896^DONOR WNV (NAT) ASSAY^L|||20150812|20150812||||||||||LALCA||06001190^BSCTA|||||F
OBX|1|ST|34892-0^LOINC^LN^138899^PROCLEIX WNV||NR||NONREACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|NONREACTIVE FOR WNV RNA
NTE|3|L|TEST PERFORMED WITH PROCLEIX WNV KIT.
NTE|4|L|.
MSH|^~\&|XYZ|MACHETELAB|MACHETE HEALTH|MACHETE HEALTH|20160112092254||ORU^R01|M16012000000000005|T|2.3|||ER|ER
PID|1|374736473647364|374736473647364||TEST4^LABCORP4||19600130|F|||9824 WYNBROOK RD^^OAKLAND^CA^21224|||||||
PV1|1|O||||||1023051000^NOGA^STEPHEN^^^^^^^^^^NPI|||||||||||||||||||||||||||||||RN
ORC||523334801660|523334801660||||||20150821|||1023051000^NOGA^STEPHEN^^^^^^^^^^NPI
OBR|1||523334801660|005009^CBC WITH DIFFERENTIAL/PLATELET^L|||20150821|20150821||||||||1023051000^DOE^STEPHEN^^^^^^^^^^NPI||RN||19770672^HELIB^913314831|||||F
OBX|1|NM|6690-2^LOINC^LN^005025^WBC||4.7|X10E3/UL|3.4-10.8||||F|||20150821|RN   ^^L
OBX|2|NM|789-8^LOINC^LN^005033^RBC||4.39|X10E6/UL|3.77-5.28||||F|||20150821|RN   ^^L
OBX|3|NM|718-7^LOINC^LN^005041^HEMOGLOBIN||11.9|G/DL|11.1-15.9||||F|||20150821|RN   ^^L
OBX|4|NM|4544-3^LOINC^LN^005058^HEMATOCRIT||35.6|%|34.0-46.6||||F|||20150821|RN   ^^L
OBX|5|NM|787-2^LOINC^LN^015065^MCV||81|FL|79-97||||F|||20150821|RN   ^^L
OBX|6|NM|785-6^LOINC^LN^015073^MCH||27.1|PG|26.6-33.0||||F|||20150821|RN   ^^L
OBX|7|NM|786-4^LOINC^LN^015081^MCHC||33.4|G/DL|31.5-35.7||||F|||20150821|RN   ^^L
OBX|8|NM|788-0^LOINC^LN^105007^RDW||14.1|%|12.3-15.4||||F|||20150821|RN   ^^L
OBX|9|NM|777-3^LOINC^LN^015172^PLATELETS||145|X10E3/UL|150-379|L|||F|||20150821|RN   ^^L
OBX|10|NM|770-8^LOINC^LN^015107^NEUTROPHILS||73|%|||||F|||20150821|RN   ^^L
OBX|11|NM|736-9^LOINC^LN^015123^LYMPHS||14|%|||||F|||20150821|RN   ^^L
OBX|12|NM|5905-5^LOINC^LN^015131^MONOCYTES||10|%|||||F|||20150821|RN   ^^L
OBX|13|NM|713-8^LOINC^LN^015149^EOS||3|%|||||F|||20150821|RN   ^^L
OBX|14|NM|706-2^LOINC^LN^015156^BASOS||0|%|||||F|||20150821|RN   ^^L
OBX|15|NM|751-8^LOINC^LN^015909^NEUTROPHILS (ABSOLUTE)||3.4|X10E3/UL|1.4-7.0||||F|||20150821|RN   ^^L
OBX|16|NM|731-0^LOINC^LN^015917^LYMPHS (ABSOLUTE)||.7|X10E3/UL|0.7-3.1||||F|||20150821|RN   ^^L
OBX|17|NM|742-7^LOINC^LN^015925^MONOCYTES(ABSOLUTE)||.5|X10E3/UL|0.1-0.9||||F|||20150821|RN   ^^L
OBX|18|NM|711-2^LOINC^LN^015933^EOS (ABSOLUTE)||.1|X10E3/UL|0.0-0.4||||F|||20150821|RN   ^^L
OBX|19|NM|704-7^LOINC^LN^015941^BASO (ABSOLUTE)||0|X10E3/UL|0.0-0.2||||F|||20150821|RN   ^^L
OBX|20|NM|38518-7^LOINC^LN^015108^IMMATURE GRANULOCYTES||0|%|||||F|||20150821|RN   ^^L
OBX|21|NM|51584-1^LOINC^LN^015911^IMMATURE GRANS (ABS)||0|X10E3/UL|0.0-0.1||||F|||20150821|RN   ^^L
OBR|2||523334801660|322000^COMP. METABOLIC PANEL (14)^L|||20150821|20150821||||||||1023051000^DOE^STEPHEN^^^^^^^^^^NPI||RN||19770672^HELIB^913314831|||||F
OBX|1|NM|2345-7^LOINC^LN^001032^GLUCOSE, SERUM||101|MG/DL|65-99|H|||F|||20150821|RN   ^^L
OBX|2|NM|3094-0^LOINC^LN^001040^BUN||12|MG/DL|6-24||||F|||20150821|RN   ^^L
OBX|3|NM|2160-0^LOINC^LN^001370^CREATININE, SERUM||.87|MG/DL|0.57-1.00||||F|||20150821|RN   ^^L
OBX|4|NM|48642-3^LOINC^LN^100791^EGFR IF NONAFRICN AM||75|ML/MIN/1.73|>59||||F|||20150821|^^L
OBX|5|NM|48643-1^LOINC^LN^100797^EGFR IF AFRICN AM||87|ML/MIN/1.73|>59||||F|||20150821|^^L
OBX|6|NM|3097-3^LOINC^LN^011577^BUN/CREATININE RATIO||14||9-23||||F|||20150821|^^L
OBX|7|NM|2951-2^LOINC^LN^001198^SODIUM, SERUM||143|MMOL/L|134-144||||F|||20150821|RN   ^^L
OBX|8|NM|2823-3^LOINC^LN^001180^POTASSIUM, SERUM||3.8|MMOL/L|3.5-5.2||||F|||20150821|RN   ^^L
OBX|9|NM|2075-0^LOINC^LN^001206^CHLORIDE, SERUM||105|MMOL/L|97-108||||F|||20150821|RN   ^^L
OBX|10|NM|2028-9^LOINC^LN^001578^CARBON DIOXIDE, TOTAL||22|MMOL/L|18-29||||F|||20150821|RN   ^^L
OBX|11|NM|17861-6^LOINC^LN^001016^CALCIUM, SERUM||9.5|MG/DL|8.7-10.2||||F|||20150821|RN   ^^L
OBX|12|NM|2885-2^LOINC^LN^001073^PROTEIN, TOTAL, SERUM||6.3|G/DL|6.0-8.5||||F|||20150821|RN   ^^L
OBX|13|NM|1751-7^LOINC^LN^001081^ALBUMIN, SERUM||4.6|G/DL|3.5-5.5||||F|||20150821|RN   ^^L
OBX|14|NM|10834-0^LOINC^LN^012039^GLOBULIN, TOTAL||1.7|G/DL|1.5-4.5||||F|||20150821|^^L
OBX|15|NM|1759-0^LOINC^LN^012047^A/G RATIO||2.7||1.1-2.5|H|||F|||20150821|^^L
OBX|16|NM|1975-2^LOINC^LN^001099^BILIRUBIN, TOTAL||.3|MG/DL|0.0-1.2||||F|||20150821|RN   ^^L
OBX|17|NM|6768-6^LOINC^LN^001107^ALKALINE PHOSPHATASE, S||92|IU/L|39-117||||F|||20150821|RN   ^^L
OBX|18|NM|1920-8^LOINC^LN^001123^AST (SGOT)||16|IU/L|0-40||||F|||20150821|RN   ^^L
OBX|19|NM|1742-6^LOINC^LN^001545^ALT (SGPT)||16|IU/L|0-32||||F|||20150821|RN   ^^L
OBR|3||523334801660|001057^URIC ACID, SERUM^L|||20150821|20150821||||||||1023051000^DOE^STEPHEN^^^^^^^^^^NPI||RN||19770672^HELIB^913314831|||||F
OBX|1|NM|3084-1^LOINC^LN^001057^URIC ACID, SERUM||3.1|MG/DL|2.5-7.1||||F|||20150821|RN   ^^L
OBR|4||523334801660|001115^LDH^L|||20150821|20150821||||||||1023051000^DOE^STEPHEN^^^^^^^^^^NPI||RN||19770672^HELIB^913314831|||||F
OBX|1|NM|2532-0^LOINC^LN^001115^LDH||165|IU/L|119-226||||F|||20150821|RN   ^^L
MSH|^~\&|LCAMC|MACHETELAB|MACHETE HEALTH|MACHETE HEALTH|20160112092254||ORU^R01|M16012000000000006|T|2.3|||ER|ER
PID|1|645374374537|645374374537||TEST5^LABCORP5||19591024|F|||93892EUE2YE8 BARKLEY AVE^^ESSEX^MD^21221|||||||
PV1|1|O||||||1417136276^COOK^ALYSSA^^^^^^^^^^NPI|||||||||||||||||||||||||||||||WB
ORC||5254C5705900|5254C5705900||||||20150911|||1417136276^COOK^ALYSSA^^^^^^^^^^NPI
OBR|1||5254C5705900|195050^PAP LB, HPV-HR^L|||20150911|20150911||||||||1417136276^DOE^ALCIA^^^^^^^^^^NPI||WB||19528512^HELIX^7667986-0|||||F
OBX|1|ST|22637-3^LOINC^LN^191108^DIAGNOSIS:||NEGATIVE FOR INTRAEPITHELIAL LESION AND MALIGNANCY.||||||F|||20150911|WB   ^^L
NTE|1|L|NEGATIVE FOR INTRAEPITHELIAL LESION AND MALIGNANCY.
OBX|2|ST|11546-9^LOINC^LN^019018||NIL||||||F|||20150911|WB   ^^L
NTE|1|L|.
OBX|3|ST|^LOINC^LN^190109^NOTE:||PAPSMR||||||F|||20150911|WB   ^^L
NTE|1|L|THE PAP SMEAR IS A SCREENING TEST DESIGNED TO AID IN THE DETECTION OF
NTE|2|L|PREMALIGNANT AND MALIGNANT CONDITIONS OF THE UTERINE CERVIX.  IT IS NOT A
NTE|3|L|DIAGNOSTIC PROCEDURE AND SHOULD NOT BE USED AS THE SOLE MEANS OF DETECTING
NTE|4|L|CERVICAL CANCER.  BOTH FALSE-POSITIVE AND FALSE-NEGATIVE REPORTS DO OCCUR.
NTE|5|L|.
OBX|4|ST|30167-1^LOINC^LN^507303^HPV, HIGH-RISK||N||NEGATIVE||||F|||20150911|=G   ^^L
NTE|1|L|NEGATIVE
MSH|^~\&|MACHETELAB|^DOSC|MACHETE HEALTH|18779|20130405125146269||ORM^O01|1999077678|P|2.3|||AL|AL
NTE|1||KOPASD
NTE|2||A3RJ
NTE|3||7ADS
NTE|4||G46DG
PID|1|000000000026|60043^^^MACHETE^MRN||MACHETE^JOE||19890909|F|||123 SEASAME STREET^^OAKLAND^CA^94600||5101234567|5101234567||||||||||||||||N
PD1|M|F|N||||F|
NTE|1||IN42
PV1|1|O|||||92383^Machete^Janice||||||||||||12345|||||||||||||||||||||||||201304051104
PV2||||||||20150615|20150616|1||||||||||||||||||||||||||N
IN1|1|||MACHETE INC|1234 Fruitvale ave^^Oakland^CA^94601^USA||5101234567^^^^^510^1234567|074394|||||||A1|MACHETE^JOE||19890909|123 SEASAME STREET^^Oakland^CA^94600||||||||||||N|||||666889999|0||||||F||||T||60043^^^MACHETE^MRN
GT1|1|60043^^^MACHETE^MRN|MACHETE^JOE||123 SEASAME STREET^^Oakland^CA^94600|5416666666|5418888888|19890909|F|P
AL1|1|FA|^pollen allergy|SV|jalubu daggu||
ORC|NW|PRO2350||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
OBR|1|PRO2350||11636^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
OBX|1||URST^Urine Specimen Type^^^||URN
NTE|1||abc
NTE|2||dsa
ORC|NW|PRO2351||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
OBR|1|PRO2350||11637^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
OBX|1||URST^Urine Specimen Type^^^||URN
NTE|1||abc
NTE|2||dsa
ORC|NW|PRO2352||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
OBR|1|PRO2350||11638^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
OBX|1||URST^Urine Specimen Type^^^||URN
NTE|1||abc
NTE|2||dsa
BTS|0000000006|B000001|0~0000187
FTS|1|F16012095417~0~0000189";

            using (var stream = new StringReader(message))
            {
                StreamText text = await new TextReaderStreamTextReader(stream, Environment.NewLine).Text;

                ParseResult<HL7Entity> result = await Parser.ParseStream(text, new TextSpan(0, text.Length));

                int i = 0;
                while (result.HasResult)
                {
                    if (result.TryGetEntity(0, out HL7Segment segment))
                        if (segment is MSH msh)
                            i++;

                    result = await result.NextAsync();
                }
                
                Assert.AreEqual(7, i);
                Console.WriteLine(i);
            }
        }
        
        [Test]
        public async Task Should_be_able_to_parse_large_message()
        {
            const string message = @"MSH|^~\&|XYZ|MACHETELAB|MACHETE HEALTH|MACHETE HEALTH|20160112092254||ORU^R01|M16012000000000001|T|2.3|||ER|ER
PID|1|7548547857847|7548547857847||TEST^MACHETELAB||19731129|F|||562 ASHBRIDGE DR, APT K^^OAKLAND^CA^94123|||||||
PV1|1|O||||||1134559404^CARTER^JOE^^^^^^^^^^NPI|||||||||||||||||||||||||||||||RN
ORC||515624304270|515624304270||||||20150605|||1134559404^CARTER^PUJEETA^^^^^^^^^^NPI
OBR|1||515624304270|028142^CBC, PLATELET, NO DIFFERENTIAL^L|||20150605|20150605||||||||1134559404^CARTER^JOE^^^^^^^^^^NPI||RN||19528512^MGHMD^6706085-02|||||F
OBX|1|NM|6690-2^LOINC^LN^005025^WBC||6|X10E3/UL|3.4-10.8||||F|||20150605|RN   ^^L
OBX|2|NM|789-8^LOINC^LN^005033^RBC||3.96|X10E6/UL|3.77-5.28||||F|||20150605|RN   ^^L
OBX|3|NM|718-7^LOINC^LN^005041^HEMOGLOBIN||11.8|G/DL|11.1-15.9||||F|||20150605|RN   ^^L
OBX|4|NM|4544-3^LOINC^LN^005058^HEMATOCRIT||35.8|%|34.0-46.6||||F|||20150605|RN   ^^L
OBX|5|NM|787-2^LOINC^LN^015065^MCV||90|FL|79-97||||F|||20150605|RN   ^^L
OBX|6|NM|785-6^LOINC^LN^015073^MCH||29.8|PG|26.6-33.0||||F|||20150605|RN   ^^L
OBX|7|NM|786-4^LOINC^LN^015081^MCHC||33|G/DL|31.5-35.7||||F|||20150605|RN   ^^L
OBX|8|NM|788-0^LOINC^LN^105007^RDW||13.7|%|12.3-15.4||||F|||20150605|RN   ^^L
OBX|9|NM|777-3^LOINC^LN^015172^PLATELETS||269|X10E3/UL|150-379||||F|||20150605|RN   ^^L
OBR|2||515624304270|322758^BASIC METABOLIC PANEL (8)^L|||20150605|20150605||||||||1134559404^CARTER^JOE^^^^^^^^^^NPI||RN||19528512^MGHMD^6706085-02|||||F
OBX|1|NM|2345-7^LOINC^LN^001032^GLUCOSE, SERUM||77|MG/DL|65-99||||F|||20150605|RN   ^^L
OBX|2|NM|3094-0^LOINC^LN^001040^BUN||11|MG/DL|6-24||||F|||20150605|RN   ^^L
OBX|3|NM|2160-0^LOINC^LN^001370^CREATININE, SERUM||.7|MG/DL|0.57-1.00||||F|||20150605|RN   ^^L
OBX|4|NM|48642-3^LOINC^LN^100791^EGFR IF NONAFRICN AM||108|ML/MIN/1.73|>59||||F|||20150605|^^L
OBX|5|NM|48643-1^LOINC^LN^100797^EGFR IF AFRICN AM||124|ML/MIN/1.73|>59||||F|||20150605|^^L
OBX|6|NM|3097-3^LOINC^LN^011577^BUN/CREATININE RATIO||16||9-23||||F|||20150605|^^L
OBX|7|NM|2951-2^LOINC^LN^001198^SODIUM, SERUM||139|MMOL/L|134-144||||F|||20150605|RN   ^^L
OBX|8|NM|2823-3^LOINC^LN^001180^POTASSIUM, SERUM||4.2|MMOL/L|3.5-5.2||||F|||20150605|RN   ^^L
OBX|9|NM|2075-0^LOINC^LN^001206^CHLORIDE, SERUM||101|MMOL/L|97-108||||F|||20150605|RN   ^^L
OBX|10|NM|2028-9^LOINC^LN^001578^CARBON DIOXIDE, TOTAL||26|MMOL/L|18-29||||F|||20150605|RN   ^^L
OBX|11|NM|17861-6^LOINC^LN^001016^CALCIUM, SERUM||8.8|MG/DL|8.7-10.2||||F|||20150605|RN   ^^L
OBR|3||515624304270|001453^HEMOGLOBIN A1C^L|||20150605|20150605||||||||1134559404^CARTER^JOE^^^^^^^^^^NPI||RN||19528512^MGHMD^6706085-02|||||F
OBX|1|NM|4548-4^LOINC^LN^001464^HEMOGLOBIN A1C||5.9|%|4.8-5.6|H|||F|||20150605|RN   ^^L
ORC||520424318740|520424318740||||||20150723|||1649421041^MUGANLINSKAYA^NARGIZ^^^^^^^^^^NPI
OBR|1||520424318740|550475^HCV GENOTYPING NON REFLEX^L|||20150723|20150723||||||||1649421041^MUGANLINSKAYA^NARGIZ^^^^^^^^^^NPI||RN||19528512^HELIB^91356570701|||||F
OBX|1|ST|32286-7^LOINC^LN^550511^HEPATITIS C GENOTYPE||HC1A||||||F|||20150723|BN   ^^L
NTE|1|L|1A
ORC||522546412110|522546412110||||||20150812|||1477550036^NULSEN^JOHN^^^^^^^^^^NPI
OBR|1||522546412110|139061^FDA GUIDANCE FEMALE WITH RFLX^L|||20150812|20150812||||||||1477550036^DOE^JOHN^^^^^^^^^^NPI||LALCA||06001190^BSCTA|||||F
OBX|1|ST|47364-5^LOINC^LN^138748^HEPATITIS B SURFACE ANTIGEN||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH BIO-RAD GENETIC SYSTEMS HEPATITIS B SURFACE
NTE|3|L|ANTIGEN KIT VERSION 3.0.
NTE|4|L|.
OBX|2|ST|47358-7^LOINC^LN^138698^HEPATITIS B CORE TOTAL AB||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ORTHO HEPATITIS B CORE TOTAL ANTIBODY KIT.
NTE|3|L|.
OBX|3|ST|47441-1^LOINC^LN^138908^HEPATITIS C VIRUS ANTIBODY||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ORTHO HEPATITIS C VIRUS ANTIBODY KIT VERSION 3.0.
NTE|3|L|.
OBX|4|ST|47361-1^LOINC^LN^139281^DONOR SYPHILIS(T PALLIDUM IGG)||NR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|TEST PERFORMED WITH TRINITY BIOTECH CAPTIA SYPHILIS
NTE|3|L|(T. PALLIDUM)-G KIT.
OBX|5|ST|59052-1^LOINC^LN^139241^HIV 1/HCV/HBV NAT||ULTNR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NONREACTIVE FOR HIV-1 RNA
NTE|2|L|NONREACTIVE FOR HCV RNA
NTE|3|L|NONREACTIVE FOR HBV DNA
NTE|4|L|TEST PERFORMED WITH NOVARTIS PROCLEIX ULTRIO ASSAY KIT.
OBX|6|ST|50411-8^LOINC^LN^138829^CHLAMYDIA, NUCLEIC ACID AMP||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
OBX|7|ST|50412-6^LOINC^LN^138830^GONOCOCCUS, NUCLEIC ACID AMP||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH GEN-PROBE APTIMA COMBO 2 KIT.
OBR|2||522546412110|138896^DONOR WNV (NAT) ASSAY^L|||20150812|20150812||||||||1477550036^DOE^JOHN^^^^^^^^^^NPI||LALCA||06001190^BSCTA|||||F
OBX|1|ST|34892-0^LOINC^LN^138899^PROCLEIX WNV||NR||NONREACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|NONREACTIVE FOR WNV RNA
NTE|3|L|TEST PERFORMED WITH PROCLEIX WNV KIT.
NTE|4|L|.
ORC||522546412150|522546412150||||||20150812
OBR|1||522546412150|139063^FDA GUIDANCE MALE WITH REFLEX^L|||20150812|20150812||||||||||LALCA||06001190^BSCTA|||||F
OBX|1|ST|47364-5^LOINC^LN^138748^HEPATITIS B SURFACE ANTIGEN||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH BIO-RAD GENETIC SYSTEMS HEPATITIS B SURFACE
NTE|3|L|ANTIGEN KIT VERSION 3.0.
NTE|4|L|.
OBX|2|ST|47358-7^LOINC^LN^138698^HEPATITIS B CORE TOTAL AB||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ORTHO HEPATITIS B CORE TOTAL ANTIBODY KIT.
NTE|3|L|.
OBX|3|ST|47441-1^LOINC^LN^138908^HEPATITIS C VIRUS ANTIBODY||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ORTHO HEPATITIS C VIRUS ANTIBODY KIT VERSION 3.0.
NTE|3|L|.
OBX|4|ST|47361-1^LOINC^LN^139281^DONOR SYPHILIS(T PALLIDUM IGG)||NR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|TEST PERFORMED WITH TRINITY BIOTECH CAPTIA SYPHILIS
NTE|3|L|(T. PALLIDUM)-G KIT.
OBX|5|ST|59052-1^LOINC^LN^139241^HIV 1/HCV/HBV NAT||ULTNR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NONREACTIVE FOR HIV-1 RNA
NTE|2|L|NONREACTIVE FOR HCV RNA
NTE|3|L|NONREACTIVE FOR HBV DNA
NTE|4|L|TEST PERFORMED WITH NOVARTIS PROCLEIX ULTRIO ASSAY KIT.
OBX|6|ST|44538-7^LOINC^LN^138781^HTLV-I/II ANTIBODIES, QUAL||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH ABBOTT PRISM HTLV-I/HTLV-II ASSAY.
OBX|7|ST|47430-4^LOINC^LN^138663^CYTOMEGALOVIRUS CMV TOTAL AB||NR||NON REACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|TEST PERFORMED WITH IMMUCOR CAPTURE-CMV IGG AND IGM KIT.
NTE|3|L|.
OBX|8|ST|50411-8^LOINC^LN^138829^CHLAMYDIA, NUCLEIC ACID AMP||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
OBX|9|ST|50412-6^LOINC^LN^138830^GONOCOCCUS, NUCLEIC ACID AMP||N||NEGATIVE||||F|||20150812|LALCA^^L
NTE|1|L|NEGATIVE
NTE|2|L|TEST PERFORMED WITH GEN-PROBE APTIMA COMBO 2 KIT.
OBR|2||522546412150|138896^DONOR WNV (NAT) ASSAY^L|||20150812|20150812||||||||||LALCA||06001190^BSCTA|||||F
OBX|1|ST|34892-0^LOINC^LN^138899^PROCLEIX WNV||NR||NONREACTIVE||||F|||20150812|LALCA^^L
NTE|1|L|NON REACTIVE
NTE|2|L|NONREACTIVE FOR WNV RNA
NTE|3|L|TEST PERFORMED WITH PROCLEIX WNV KIT.
NTE|4|L|.
ORC||523334801660|523334801660||||||20150821|||1023051000^NOGA^STEPHEN^^^^^^^^^^NPI
OBR|1||523334801660|005009^CBC WITH DIFFERENTIAL/PLATELET^L|||20150821|20150821||||||||1023051000^DOE^STEPHEN^^^^^^^^^^NPI||RN||19770672^HELIB^913314831|||||F
OBX|1|NM|6690-2^LOINC^LN^005025^WBC||4.7|X10E3/UL|3.4-10.8||||F|||20150821|RN   ^^L
OBX|2|NM|789-8^LOINC^LN^005033^RBC||4.39|X10E6/UL|3.77-5.28||||F|||20150821|RN   ^^L
OBX|3|NM|718-7^LOINC^LN^005041^HEMOGLOBIN||11.9|G/DL|11.1-15.9||||F|||20150821|RN   ^^L
OBX|4|NM|4544-3^LOINC^LN^005058^HEMATOCRIT||35.6|%|34.0-46.6||||F|||20150821|RN   ^^L
OBX|5|NM|787-2^LOINC^LN^015065^MCV||81|FL|79-97||||F|||20150821|RN   ^^L
OBX|6|NM|785-6^LOINC^LN^015073^MCH||27.1|PG|26.6-33.0||||F|||20150821|RN   ^^L
OBX|7|NM|786-4^LOINC^LN^015081^MCHC||33.4|G/DL|31.5-35.7||||F|||20150821|RN   ^^L
OBX|8|NM|788-0^LOINC^LN^105007^RDW||14.1|%|12.3-15.4||||F|||20150821|RN   ^^L
OBX|9|NM|777-3^LOINC^LN^015172^PLATELETS||145|X10E3/UL|150-379|L|||F|||20150821|RN   ^^L
OBX|10|NM|770-8^LOINC^LN^015107^NEUTROPHILS||73|%|||||F|||20150821|RN   ^^L
OBX|11|NM|736-9^LOINC^LN^015123^LYMPHS||14|%|||||F|||20150821|RN   ^^L
OBX|12|NM|5905-5^LOINC^LN^015131^MONOCYTES||10|%|||||F|||20150821|RN   ^^L
OBX|13|NM|713-8^LOINC^LN^015149^EOS||3|%|||||F|||20150821|RN   ^^L
OBX|14|NM|706-2^LOINC^LN^015156^BASOS||0|%|||||F|||20150821|RN   ^^L
OBX|15|NM|751-8^LOINC^LN^015909^NEUTROPHILS (ABSOLUTE)||3.4|X10E3/UL|1.4-7.0||||F|||20150821|RN   ^^L
OBX|16|NM|731-0^LOINC^LN^015917^LYMPHS (ABSOLUTE)||.7|X10E3/UL|0.7-3.1||||F|||20150821|RN   ^^L
OBX|17|NM|742-7^LOINC^LN^015925^MONOCYTES(ABSOLUTE)||.5|X10E3/UL|0.1-0.9||||F|||20150821|RN   ^^L
OBX|18|NM|711-2^LOINC^LN^015933^EOS (ABSOLUTE)||.1|X10E3/UL|0.0-0.4||||F|||20150821|RN   ^^L
OBX|19|NM|704-7^LOINC^LN^015941^BASO (ABSOLUTE)||0|X10E3/UL|0.0-0.2||||F|||20150821|RN   ^^L
OBX|20|NM|38518-7^LOINC^LN^015108^IMMATURE GRANULOCYTES||0|%|||||F|||20150821|RN   ^^L
OBX|21|NM|51584-1^LOINC^LN^015911^IMMATURE GRANS (ABS)||0|X10E3/UL|0.0-0.1||||F|||20150821|RN   ^^L
OBR|2||523334801660|322000^COMP. METABOLIC PANEL (14)^L|||20150821|20150821||||||||1023051000^DOE^STEPHEN^^^^^^^^^^NPI||RN||19770672^HELIB^913314831|||||F
OBX|1|NM|2345-7^LOINC^LN^001032^GLUCOSE, SERUM||101|MG/DL|65-99|H|||F|||20150821|RN   ^^L
OBX|2|NM|3094-0^LOINC^LN^001040^BUN||12|MG/DL|6-24||||F|||20150821|RN   ^^L
OBX|3|NM|2160-0^LOINC^LN^001370^CREATININE, SERUM||.87|MG/DL|0.57-1.00||||F|||20150821|RN   ^^L
OBX|4|NM|48642-3^LOINC^LN^100791^EGFR IF NONAFRICN AM||75|ML/MIN/1.73|>59||||F|||20150821|^^L
OBX|5|NM|48643-1^LOINC^LN^100797^EGFR IF AFRICN AM||87|ML/MIN/1.73|>59||||F|||20150821|^^L
OBX|6|NM|3097-3^LOINC^LN^011577^BUN/CREATININE RATIO||14||9-23||||F|||20150821|^^L
OBX|7|NM|2951-2^LOINC^LN^001198^SODIUM, SERUM||143|MMOL/L|134-144||||F|||20150821|RN   ^^L
OBX|8|NM|2823-3^LOINC^LN^001180^POTASSIUM, SERUM||3.8|MMOL/L|3.5-5.2||||F|||20150821|RN   ^^L
OBX|9|NM|2075-0^LOINC^LN^001206^CHLORIDE, SERUM||105|MMOL/L|97-108||||F|||20150821|RN   ^^L
OBX|10|NM|2028-9^LOINC^LN^001578^CARBON DIOXIDE, TOTAL||22|MMOL/L|18-29||||F|||20150821|RN   ^^L
OBX|11|NM|17861-6^LOINC^LN^001016^CALCIUM, SERUM||9.5|MG/DL|8.7-10.2||||F|||20150821|RN   ^^L
OBX|12|NM|2885-2^LOINC^LN^001073^PROTEIN, TOTAL, SERUM||6.3|G/DL|6.0-8.5||||F|||20150821|RN   ^^L
OBX|13|NM|1751-7^LOINC^LN^001081^ALBUMIN, SERUM||4.6|G/DL|3.5-5.5||||F|||20150821|RN   ^^L
OBX|14|NM|10834-0^LOINC^LN^012039^GLOBULIN, TOTAL||1.7|G/DL|1.5-4.5||||F|||20150821|^^L
OBX|15|NM|1759-0^LOINC^LN^012047^A/G RATIO||2.7||1.1-2.5|H|||F|||20150821|^^L
OBX|16|NM|1975-2^LOINC^LN^001099^BILIRUBIN, TOTAL||.3|MG/DL|0.0-1.2||||F|||20150821|RN   ^^L
OBX|17|NM|6768-6^LOINC^LN^001107^ALKALINE PHOSPHATASE, S||92|IU/L|39-117||||F|||20150821|RN   ^^L
OBX|18|NM|1920-8^LOINC^LN^001123^AST (SGOT)||16|IU/L|0-40||||F|||20150821|RN   ^^L
OBX|19|NM|1742-6^LOINC^LN^001545^ALT (SGPT)||16|IU/L|0-32||||F|||20150821|RN   ^^L
OBR|3||523334801660|001057^URIC ACID, SERUM^L|||20150821|20150821||||||||1023051000^DOE^STEPHEN^^^^^^^^^^NPI||RN||19770672^HELIB^913314831|||||F
OBX|1|NM|3084-1^LOINC^LN^001057^URIC ACID, SERUM||3.1|MG/DL|2.5-7.1||||F|||20150821|RN   ^^L
OBR|4||523334801660|001115^LDH^L|||20150821|20150821||||||||1023051000^DOE^STEPHEN^^^^^^^^^^NPI||RN||19770672^HELIB^913314831|||||F
OBX|1|NM|2532-0^LOINC^LN^001115^LDH||165|IU/L|119-226||||F|||20150821|RN   ^^L
ORC||5254C5705900|5254C5705900||||||20150911|||1417136276^COOK^ALYSSA^^^^^^^^^^NPI
OBR|1||5254C5705900|195050^PAP LB, HPV-HR^L|||20150911|20150911||||||||1417136276^DOE^ALCIA^^^^^^^^^^NPI||WB||19528512^HELIX^7667986-0|||||F
OBX|1|ST|22637-3^LOINC^LN^191108^DIAGNOSIS:||NEGATIVE FOR INTRAEPITHELIAL LESION AND MALIGNANCY.||||||F|||20150911|WB   ^^L
NTE|1|L|NEGATIVE FOR INTRAEPITHELIAL LESION AND MALIGNANCY.
OBX|2|ST|11546-9^LOINC^LN^019018||NIL||||||F|||20150911|WB   ^^L
NTE|1|L|.
OBX|3|ST|^LOINC^LN^190109^NOTE:||PAPSMR||||||F|||20150911|WB   ^^L
NTE|1|L|THE PAP SMEAR IS A SCREENING TEST DESIGNED TO AID IN THE DETECTION OF
NTE|2|L|PREMALIGNANT AND MALIGNANT CONDITIONS OF THE UTERINE CERVIX.  IT IS NOT A
NTE|3|L|DIAGNOSTIC PROCEDURE AND SHOULD NOT BE USED AS THE SOLE MEANS OF DETECTING
NTE|4|L|CERVICAL CANCER.  BOTH FALSE-POSITIVE AND FALSE-NEGATIVE REPORTS DO OCCUR.
NTE|5|L|.
OBX|4|ST|30167-1^LOINC^LN^507303^HPV, HIGH-RISK||N||NEGATIVE||||F|||20150911|=G   ^^L
NTE|1|L|NEGATIVE
ORC|NW|PRO2350||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
OBR|1|PRO2350||11636^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
OBX|1||URST^Urine Specimen Type^^^||URN
NTE|1||abc
NTE|2||dsa
ORC|NW|PRO2351||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
OBR|1|PRO2350||11637^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
OBX|1||URST^Urine Specimen Type^^^||URN
NTE|1||abc
NTE|2||dsa
ORC|NW|PRO2352||XO934N|||^^^^^R||20130405125144|91238^Machete^Joe||92383^Machete^Janice
OBR|1|PRO2350||11638^Urinalysis, with Culture if Indicated^L|||20130405135133||||N|||||92383^Machete^Janice|||||||||||^^^^^R
DG1|1|I9|788.64^URINARY HESITANCY^I9|URINARY HESITANCY
OBX|1||URST^Urine Specimen Type^^^||URN
NTE|1||abc
NTE|2||dsa";

            using (var stream = new StringReader(message))
            {
                StreamText text = await new TextReaderStreamTextReader(stream, Environment.NewLine).Text;

                ParseResult<HL7Entity> result = await Parser.ParseStream(text, new TextSpan(0, text.Length));

                int index = 0;
                int segments = 0;
                int messages = 0;
                while (result.HasResult)
                {
                    while (result.TryGetEntity(index, out HL7Segment segment))
                    {
                        segments++;
                        index++;
                        
                        if (segment is MSH)
                            messages++;
                    }
                    
                    result = await result.NextAsync();
                }
                
                Assert.AreEqual(1, messages);
                Assert.AreEqual(188, segments);
            }
        }

        [Test, Explicit]
        public async Task Test()
        {
            using (var fsStream = File.OpenRead("/users/albert/Documents/BigFile"))
            using (var stream = new StreamReader(fsStream))
            {
                StreamText text = await new TextReaderStreamTextReader(stream, Environment.NewLine).Text;

                ParseResult<HL7Entity> result = await Parser.ParseStream(text, new TextSpan(0, text.Length));

                int index = 0;
                int segments = 0;
                int messages = 0;
                while (result.HasResult)
                {
                    while (result.TryGetEntity(index, out HL7Segment segment))
                    {
                        segments++;
                        index++;
                        
                        if (segment is MSH)
                            messages++;
                    }
                    
                    result = await result.NextAsync();
                }
                
                Assert.AreEqual(1, messages);
//                Assert.AreEqual(188, segments);
                Console.WriteLine(segments);
            }
            
        }

    }
}