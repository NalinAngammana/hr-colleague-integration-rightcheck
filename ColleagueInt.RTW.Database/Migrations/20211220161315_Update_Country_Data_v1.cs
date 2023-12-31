﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ColleagueInt.RTW.Database.Migrations
{
    public partial class Update_Country_Data_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RTWCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RTWCountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HCMCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HCMCountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "HCMCountryCode", "HCMCountryName", "RTWCountryCode", "RTWCountryName" },
                values: new object[,]
                {
                    { 1, "AX", "Aland Islands", "ALA", "Aaland Islands" },
                    { 159, "NF", "Norfolk Island", "NFK", "Norfolk Island" },
                    { 160, "MP", "Northern Mariana Islands", "MNP", "Northern Mariana Islands" },
                    { 161, "MK", "North Macedonia", "MKD", "Macedonia" },
                    { 162, "NO", "Norway", "NOR", "Norway" },
                    { 163, "OM", "Oman", "OMN", "Oman" },
                    { 164, "PK", "Pakistan", "PAK", "Pakistan" },
                    { 165, "PW", "Palau", "PLW", "Palau" },
                    { 166, "PS", "Palestine, State of", "PSE", "Palestine" },
                    { 167, "PA", "Panama", "PAN", "Panama" },
                    { 168, "PG", "Papua New Guinea", "PNG", "Papua New Guinea" },
                    { 169, "PY", "Paraguay", "PRY", "Paraguay" },
                    { 170, "PE", "Peru", "PER", "Peru" },
                    { 171, "PH", "Philippines", "PHL", "Philippines" },
                    { 172, "PN", "Pitcairn", "PCN", "Pitcairn" },
                    { 173, "PL", "Poland", "POL", "Poland" },
                    { 174, "PT", "Portugal", "PRT", "Portugal" },
                    { 175, "PR", "Puerto Rico", "PRI", "Puerto Rico" },
                    { 176, "QA", "Qatar", "QAT", "Qatar" },
                    { 177, "RE", "Reunion", "REU", "Reunion" },
                    { 178, "RO", "Romania", "ROU", "Romania" },
                    { 179, "RU", "Russian Federation", "RUS", "Russia" },
                    { 180, "RW", "Rwanda", "RWA", "Rwanda" },
                    { 181, "SH", "Saint Helena, Ascension and Tristan da Cunha", "SHN", "Saint Helena" },
                    { 182, "KN", "Saint Kitts and Nevis", "KNA", "Saint Kitts & Nevis" },
                    { 183, "LC", "Saint Lucia", "LCA", "Saint Lucia" },
                    { 184, "PM", "Saint Pierre and Miquelon", "SPM", "Saint Pierre et Miquelon" },
                    { 185, "VC", "Saint Vincent and the Grenadines", "VCT", "Saint Vincent & the Grenadines" },
                    { 158, "NU", "Niue", "NIU", "Niue" },
                    { 186, "WS", "Samoa", "ASM", "Samoa" },
                    { 157, "NG", "Nigeria", "NGA", "Nigeria" },
                    { 155, "NI", "Nicaragua", "NIC", "Nicaragua" },
                    { 128, "MV", "Maldives", "MDV", "Maldives" },
                    { 129, "ML", "Mali", "TCD", "Mali" },
                    { 130, "ML", "Mali", "MLI", "Mali" },
                    { 131, "MT", "Malta", "MLT", "Malta" },
                    { 132, "MH", "Marshall Islands", "MHL", "Marshall Islands" },
                    { 133, "MQ", "Martinique", "MTQ", "Martinique" },
                    { 134, "MR", "Mauritania", "MRT", "Mauritania" },
                    { 135, "MU", "Mauritius", "MUS", "Mauritius" },
                    { 136, "YT", "Mayotte", "MYT", "Mayott" },
                    { 137, "MX", "Mexico", "MEX", "Mexico" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "HCMCountryCode", "HCMCountryName", "RTWCountryCode", "RTWCountryName" },
                values: new object[,]
                {
                    { 138, "FM", "Micronesia, Federated States of", "FSM", "Micronesia" },
                    { 139, "MD", "Moldova", "MDA", "Moldova" },
                    { 140, "MC", "Monaco", "MCO", "Monaco" },
                    { 141, "MN", "Mongolia", "MNG", "Mongolia" },
                    { 142, "ME", "Montenegro", "MNE", "Montenegro" },
                    { 143, "MS", "Montserrat", "MSR", "Montserrat" },
                    { 144, "MA", "Morocco", "MAR", "Morocco" },
                    { 145, "MZ", "Mozambique", "MOZ", "Mozambique" },
                    { 146, "MM", "Myanmar", "BUR", "Myanmar" },
                    { 147, "MM", "Myanmar", "MMR", "Burma" },
                    { 148, "NA", "Namibia", "NAM", "Namibia" },
                    { 149, "NR", "Nauru", "NRU", "Nauru" },
                    { 150, "NP", "Nepal", "NPL", "Nepal" },
                    { 151, "NL", "Netherlands", "NLD", "Netherlands" },
                    { 152, "AN", "Netherlands Antilles", "ANT", "Netherlands Antilles" },
                    { 153, "NC", "New Caledonia", "NCL", "New Caledonia" },
                    { 154, "NZ", "New Zealand", "NZL", "New Zealand" },
                    { 156, "NE", "Niger", "NER", "Niger" },
                    { 187, "WS", "Samoa", "WSM", "Samoa" },
                    { 188, "SM", "San Marino", "SMR", "San Marino" },
                    { 189, "ST", "Sao Tome and Principe", "STP", "Sao Tome et Principe" },
                    { 222, "TM", "Turkmenistan", "TKM", "Turkmenistan" },
                    { 223, "TC", "Turks and Caicos Islands", "TCA", "Turks & Caicos Islands" },
                    { 224, "TV", "Tuvalu", "TUV", "Tuvalu" },
                    { 225, "UG", "Uganda", "UGA", "Uganda" },
                    { 226, "UA", "Ukraine", "UKR", "Ukraine" },
                    { 227, "AE", "United Arab Emirates", "ARE", "UAE" },
                    { 228, "GB", "United Kingdom", "GBR", "United Kingdom" },
                    { 229, "GB", "United Kingdom", "GBN", "British National (Overseas)" },
                    { 230, "GB", "United Kingdom", "GBO", "British Overseas Citizen" },
                    { 231, "GB", "United Kingdom", "GBD", "British Overseas Territories Citizen" },
                    { 232, "GB", "United Kingdom", "GBP", "British Protected Person" },
                    { 233, "GB", "United Kingdom", "GBS", "British Subject" },
                    { 234, "US", "United States", "USA", "United States" },
                    { 235, "UM", "United States Minor Outlying Islands", "UMI", "United States Minor Outlying Islands" },
                    { 236, "UY", "Uruguay", "URY", "Uruguay" },
                    { 237, "UZ", "Uzbekistan", "UZB", "Uzbekistan" },
                    { 238, "VU", "Vanuatu", "VUT", "Vanuatu" },
                    { 239, "VE", "Venezuela", "VEN", "Venezuela" },
                    { 240, "VN", "Viet Nam", "VNM", "Vietnam" },
                    { 241, "VG", "Virgin Islands, British", "VGB", "Virgin Islands (British)" },
                    { 242, "VI", "Virgin Islands, U.S.", "VIR", "Virgin Islands (US)" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "HCMCountryCode", "HCMCountryName", "RTWCountryCode", "RTWCountryName" },
                values: new object[,]
                {
                    { 243, "WF", "Wallis and Futuna", "WLF", "Wallis & Futuna Islands" },
                    { 244, "EH", "Western Sahara", "ESH", "Western Sahara" },
                    { 245, "YE", "Yemen", "YEM", "Yemen" },
                    { 246, "ZM", "Zambia", "ZMB", "Zambia" },
                    { 247, "ZW", "Zimbabwe", "ZWE", "Zimbabwe" },
                    { 248, "ZW", "Zimbabwe", "ZIM", "Zimbabwe" },
                    { 221, "TR", "Turkey", "TUR", "Turkey" },
                    { 220, "TN", "Tunisia", "TUN", "Tunisia" },
                    { 219, "TT", "Trinidad and Tobago", "TTO", "Trinidad & Tobago" },
                    { 218, "TO", "Tonga", "TON", "Tonga" },
                    { 190, "SA", "Saudi Arabia", "SAU", "Saudi Arabia" },
                    { 191, "SN", "Senegal", "SEN", "Senegal" },
                    { 192, "RS", "Serbia", "SRB", "Serbia" },
                    { 193, "SC", "Seychelles", "SYC", "Seychelles" },
                    { 194, "SL", "Sierra Leone", "SLE", "Sierra Leone" },
                    { 195, "SG", "Singapore", "SGP", "Singapore" },
                    { 196, "SK", "Slovakia", "SVK", "Slovakia" },
                    { 197, "SI", "Slovenia", "SVN", "Slovenia" },
                    { 198, "SB", "Solomon Islands", "SLB", "Solomon Islands" },
                    { 199, "SO", "Somalia", "SOM", "Somalia" },
                    { 200, "ZA", "South Africa", "ZAF", "South africa" },
                    { 201, "GS", "South Georgia and the South Sandwich Islands", "SGS", "South Georgia" },
                    { 202, "SS", "South Sudan", "SSD", "South Sudan" },
                    { 127, "MY", "Malaysia", "MYS", "Malaysia" },
                    { 203, "ES", "Spain", "ESP", "Spain" },
                    { 205, "SD", "Sudan", "SDN", "Sudan" },
                    { 206, "SR", "Suriname", "SUR", "Suriname" },
                    { 207, "SJ", "Svalbard and Jan Mayen", "SJM", "Svalbard & Jan Mayen Islands" },
                    { 208, "SE", "Sweden", "SWE", "Sweden" },
                    { 209, "CH", "Switzerland", "CHE", "Switzerland" },
                    { 210, "SY", "Syrian Arab Republic", "SYR", "Syria" },
                    { 211, "TW", "Taiwan", "TWN", "Taiwan" },
                    { 212, "TJ", "Tajikistan", "TJK", "Tajikistan" },
                    { 213, "TZ", "Tanzania, United Republic of", "TZA", "Tanzania" },
                    { 214, "TH", "Thailand", "THA", "Thailand" },
                    { 215, "TL", "Timor-Leste", "TLS", "Timor-Leste" },
                    { 216, "TG", "Togo", "TGO", "Togo" },
                    { 217, "TK", "Tokelau", "TKL", "Tokelau" },
                    { 204, "LK", "Sri Lanka", "LKA", "Sri Lanka" },
                    { 126, "MW", "Malawi", "MWI", "Malawi" },
                    { 125, "MG", "Madagascar", "MDG", "Madagascar" },
                    { 124, "MO", "Macao", "MAC", "Macau" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "HCMCountryCode", "HCMCountryName", "RTWCountryCode", "RTWCountryName" },
                values: new object[,]
                {
                    { 34, "BF", "Burkina Faso", "BFA", "Burkina Faso" },
                    { 35, "BI", "Burundi", "BDI", "Burundi" },
                    { 36, "KH", "Cambodia", "KHM", "Cambodia" },
                    { 37, "CM", "Cameroon", "CMR", "Cameroon" },
                    { 38, "CA", "Canada", "CAN", "Canada" },
                    { 39, "CV", "Cabo Verde", "CPV", "Cape Verde" },
                    { 40, "KY", "Cayman Islands", "CYM", "Cayman Islands" },
                    { 41, "CF", "Central African Republic", "CAF", "Central African Republic" },
                    { 42, "CL", "Chile", "CHL", "Chile" },
                    { 43, "CN", "China", "CHN", "China" },
                    { 44, "CX", "Christmas Island", "CXR", "Christmas Island" },
                    { 45, "CC", "Cocos (Keeling) Islands", "CCK", "Cocos (Keeling) Islands" },
                    { 46, "CO", "Colombia", "COL", "Colombia" },
                    { 47, "KM", "Comoros", "COM", "Comoros" },
                    { 48, "CG", "Congo-Brazzaville", "COG", "Congo, Republic of" },
                    { 49, "CG", "Congo-Brazzaville", "COD", "Congo, Democratic Republic" },
                    { 50, "CK", "Cook Islands", "COK", "Cook Islands" },
                    { 51, "CR", "Costa Rica", "CRI", "Costa Rica" },
                    { 52, "CI", "Cote d'Ivoire", "CIV", "Cte d'Ivoire" },
                    { 53, "HR", "Croatia", "HRV", "Croatia (local name: Hrvatska)" },
                    { 54, "CU", "Cuba", "CUB", "Cuba" },
                    { 55, "CY", "Cyprus", "CYP", "Cyprus" },
                    { 56, "CZ", "Czech Republic", "CZE", "Czech Republic" },
                    { 57, "DK", "Denmark", "DNK", "Denmark" },
                    { 58, "DJ", "Djibouti", "DJI", "Djibouti" },
                    { 59, "DM", "Dominica", "DMA", "Dominica" },
                    { 60, "DO", "Dominican Republic", "DOM", "Dominican Republic" },
                    { 33, "BG", "Bulgaria", "BGR", "Bulgaria" },
                    { 32, "BN", "Brunei Darussalam", "BRN", "Brunei Darussalam" },
                    { 31, "IO", "British Indian Ocean Territory", "IOT", "British Indian Ocean Territory" },
                    { 30, "BR", "Brazil", "BRA", "Brazil" },
                    { 2, "AF", "Afghanistan", "AFG", "Afghanistan" },
                    { 3, "AL", "Albania", "ALB", "Albania" },
                    { 4, "DZ", "Algeria", "DZA", "Algeria" },
                    { 5, "AD", "Andorra", "AND", "Andorra" },
                    { 6, "AO", "Angola", "AGO", "Angola" },
                    { 7, "AI", "Anguilla", "AIA", "Anguilla" },
                    { 8, "AQ", "Antarctica", "ATA", "Antarctica" },
                    { 9, "AG", "Antigua and Barbuda", "ATG", "Antigua & Barbuda" },
                    { 10, "AR", "Argentina", "ARG", "Argentina" },
                    { 11, "AM", "Armenia", "ARM", "Armenia" },
                    { 12, "AW", "Aruba", "ABW", "Aruba" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "HCMCountryCode", "HCMCountryName", "RTWCountryCode", "RTWCountryName" },
                values: new object[,]
                {
                    { 13, "AU", "Australia", "AUS", "Australia" },
                    { 14, "AT", "Austria", "AUT", "Austria" },
                    { 61, "EC", "Ecuador", "ECU", "Ecuador" },
                    { 15, "AZ", "Azerbaijan", "AZE", "Azerbaijan" },
                    { 17, "BH", "Bahrain", "BHR", "Bahrain" },
                    { 18, "BD", "Bangladesh", "BGD", "Bangladesh" },
                    { 19, "BB", "Barbados", "BRB", "Barbados" },
                    { 20, "BY", "Belarus", "BLR", "Belarus" },
                    { 21, "BE", "Belgium", "BEL", "Belgium" },
                    { 22, "BZ", "Belize", "BLZ", "Belize" },
                    { 23, "BJ", "Benin", "BEN", "Benin" },
                    { 24, "BM", "Bermuda", "BMU", "Bermuda" },
                    { 25, "BT", "Bhutan", "BTN", "Bhutan" },
                    { 26, "BO", "Bolivia", "BOL", "Bolivia" },
                    { 27, "BA", "Bosnia and Herzegovina", "BIH", "Bosnia & Herzegovina" },
                    { 28, "BW", "Botswana", "BWA", "Botswana" },
                    { 29, "BV", "Bouvet Island", "BVT", "Bouvet Island" },
                    { 16, "BS", "Bahamas", "BHS", "Bahamas" },
                    { 249, "VA", "Holy See (Vatican City State)", "VAT", "Vatican City" },
                    { 62, "EG", "Egypt", "EGY", "Egypt" },
                    { 64, "GQ", "Equatorial Guinea", "GNQ", "Equatorial Guinea" },
                    { 97, "IN", "India", "IND", "India" },
                    { 98, "ID", "Indonesia", "IDN", "Indonesia" },
                    { 99, "IR", "Iran, Islamic Republic of", "IRN", "Iran" },
                    { 100, "IQ", "Iraq", "IRQ", "Iraq" },
                    { 101, "IE", "Ireland", "IRL", "Ireland" },
                    { 102, "IL", "Israel", "ISR", "Israel" },
                    { 103, "IT", "Italy", "ITA", "Italy" },
                    { 104, "JM", "Jamaica", "JAM", "Jamaica" },
                    { 105, "JP", "Japan", "JPN", "Japan" },
                    { 106, "JO", "Jordan", "JOR", "Jordan" },
                    { 107, "KZ", "Kazakhstan", "KAZ", "Kazakhstan" },
                    { 108, "KE", "Kenya", "KEN", "Kenya" },
                    { 109, "KI", "Kiribati", "KIR", "Kiribati" },
                    { 110, "KP", "Korea, Democratic People's Republic of", "KOR", "Korea" },
                    { 111, "KR", "Korea, Republic of", "PRK", "Korea, Democratic People's Republic" },
                    { 112, "XK", "Kosovo", "RKS", "Kosovo" },
                    { 113, "KG", "Kyrgyzstan", "KGZ", "Krygyzstan" },
                    { 114, "KW", "Kuwait", "KWT", "Kuwait" },
                    { 115, "LA", "Lao People's Democratic Republic", "LAO", "Laos" },
                    { 116, "LV", "Latvia", "LVA", "Latvia" },
                    { 117, "LB", "Lebanon", "LBN", "Lebanon" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "HCMCountryCode", "HCMCountryName", "RTWCountryCode", "RTWCountryName" },
                values: new object[,]
                {
                    { 118, "LS", "Lesotho", "LSO", "Lesotho" },
                    { 119, "LR", "Liberia", "LBR", "Liberia" },
                    { 120, "LY", "Libya", "LBY", "Libya" },
                    { 121, "LI", "Liechtenstein", "LIE", "Liechtenstein" },
                    { 122, "LT", "Lithuania", "LTU", "Lithuania" },
                    { 123, "LU", "Luxembourg", "LUX", "Luxembourg" },
                    { 96, "IS", "Iceland", "ISL", "Iceland" },
                    { 95, "HU", "Hungary", "HUN", "Hungary" },
                    { 94, "HK", "Hong Kong", "HKG", "Hong Kong" },
                    { 93, "HN", "Honduras", "HND", "Honduras" },
                    { 65, "ER", "Eritrea", "ERI", "Eritrea" },
                    { 66, "EE", "Estonia", "EST", "Estonia" },
                    { 67, "ET", "Ethiopia", "ETH", "Ethiopia" },
                    { 68, "FK", "Falkland Islands (Malvinas)", "FLK", "Falkland Islands (Malvinas)" },
                    { 69, "FO", "Faroe Islands", "FRO", "Faroe Islands" },
                    { 70, "FJ", "Fiji", "FJI", "Fiji" },
                    { 71, "FI", "Finland", "FIN", "Finland" },
                    { 72, "FR", "France", "FRA", "France" },
                    { 73, "GF", "French Guiana", "ATF", "Franch Southern Territories" },
                    { 74, "PF", "French Polynesia", "GUF", "French Guiana" },
                    { 75, "TF", "French Southern Territories", "PYF", "French Polynesia" },
                    { 76, "GA", "Gabon", "GAB", "Gabon" },
                    { 77, "GM", "Gambia", "GMB", "Gambia" },
                    { 63, "SV", "El Salvador", "SLV", "El Salvador" },
                    { 78, "GE", "Georgia", "GEO", "Georgia" },
                    { 80, "GH", "Ghana", "GHA", "Ghana" },
                    { 81, "GI", "Gibraltar", "GIB", "Gibraltar" },
                    { 82, "GR", "Greece", "GRC", "Greece" },
                    { 83, "GL", "Greenland", "GRL", "Greenland" },
                    { 84, "GD", "Grenada", "GRD", "Grenada" },
                    { 85, "GP", "Guadeloupe", "GLP", "Guadeloupe" },
                    { 86, "GU", "Guam", "GUM", "Guam" },
                    { 87, "GT", "Guatemala", "GTM", "Guatemala" },
                    { 88, "GN", "Guinea", "GIN", "Guinea" },
                    { 89, "GW", "Guinea-Bissau", "GNB", "Guinea-Bissau" },
                    { 90, "GY", "Guyana", "GUY", "Guyana" },
                    { 91, "HT", "Haiti", "HTI", "Haiti" },
                    { 92, "HM", "Heard Island and McDonald Islands", "HMD", "Heard & McDonald Islands" },
                    { 79, "DE", "Germany", "DEU", "Germany" },
                    { 250, "SZ", "Eswatini", "SWZ", "Swaziland" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
