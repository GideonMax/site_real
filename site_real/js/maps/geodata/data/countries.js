var countries = {
    "AL": ["albaniaLow", "albaniaHigh"],
    "DZ": ["algeriaLow", "algeriaHigh"],
    "AS": ["americanSamoaLow", "americanSamoaHigh"],
    "AD": ["andorraLow", "andorraHigh"],
    "AO": ["angolaLow", "angolaHigh"],
    "AI": ["anguillaLow", "anguillaHigh"],
    "AG": ["antiguaBarbudaLow", "antiguaBarbudaHigh"],
    "AR": ["argentinaLow", "argentinaHigh"],
    "AM": ["armeniaLow", "armeniaHigh"],
    "AW": ["arubaLow", "arubaHigh"],
    "AU": ["australiaLow", "australiaHigh"],
    "AT": ["austriaLow", "austriaHigh"],
    "AZ": ["azerbaijanLow", "azerbaijanHigh"],
    "BH": ["bahrainLow", "bahrainHigh"],
    "BD": ["bangladeshLow", "bangladeshHigh"],
    "BB": ["barbadosLow", "barbadosHigh"],
    "BY": ["belarusLow", "belarusHigh"],
    "BE": ["belgiumLow", "belgiumHigh"],
    "BZ": ["belizeLow", "belizeHigh"],
    "BJ": ["beninLow", "beninHigh"],
    "BM": ["bermudaLow", "bermudaHigh"],
    "BT": ["bhutanLow", "bhutanHigh"],
    "BO": ["boliviaLow", "boliviaHigh"],
    "BQ": ["bonaireSintEustatiusSabaLow", "bonaireSintEustatiusSabaHigh"],
    "BA": ["bosniaHerzegovinaLow", "bosniaHerzegovinaHigh", "bosniaHerzegovinaCantonsLow", "bosniaHerzegovinaCantonsHigh"],
    "BW": ["botswanaLow", "botswanaHigh"],
    "BR": ["brazilLow", "brazilHigh"],
    "IO": ["britishIndianOceanTerritoryLow", "britishIndianOceanTerritoryHigh"],
    "BN": ["bruneiDarussalamLow", "bruneiDarussalamHigh"],
    "BG": ["bulgariaLow", "bulgariaHigh"],
    "BF": ["burkinaFasoLow", "burkinaFasoHigh"],
    "BI": ["burundiLow", "burundiHigh"],
    "BS": ["bahamasLow", "bahamasHigh"],
    "KH": ["cambodiaLow", "cambodiaHigh"],
    "CM": ["cameroonLow", "cameroonHigh"],
    "CA": ["canadaLow", "canadaHigh"],
    "KY": ["caymanIslandsLow", "caymanIslandsHigh"],
    "CV": ["capeVerdeLow", "capeVerdeHigh"],
    "CF": ["centralAfricanRepublicLow", "centralAfricanRepublicHigh"],
    "TD": ["chadLow", "chadHigh"],
    "GB-CHA": ["channelIslandsLow", "channelIslandsHigh"],
    "CL": ["chileLow", "chileHigh"],
    "CN": ["chinaLow", "chinaHigh"],
    "CC": ["cocosKeelingLow", "cocosKeelingHigh"],
    "CO": ["colombiaLow", "colombiaHigh", "colombiaMuniLow", "colombiaMuniHigh"],
    "KM": ["comorosLow", "comorosHigh"],
    "CD": ["congoDRLow", "congoDRHigh"],
    "CG": ["congoLow", "congoHigh"],
    "CR": ["costaRicaLow", "costaRicaHigh"],
    "HR": ["croatiaLow", "croatiaHigh"],
    "CW": ["curacaoLow", "curacaoHigh"],
    "CY": ["cyprusLow", "cyprusHigh", "cyprusNorthCyprusLow", "cyprusNorthCyprusHigh"],
    "CZ": ["czechiaLow", "czechiaHigh"],
    "DK": ["denmarkLow", "denmarkHigh"],
    "DJ": ["djiboutiLow", "djiboutiHigh"],
    "DM": ["dominicaLow", "dominicaHigh"],
    "DO": ["dominicanRepublicLow", "dominicanRepublicHigh", "dominicanRepublicMuniLow", "dominicanRepublicMuniHigh"],
    "EC": ["ecuadorLow", "ecuadorHigh"],
    "EG": ["egyptLow", "egyptHigh"],
    "SV": ["elSalvadorLow", "elSalvadorHigh"],
    "GQ": ["equatorialGuineaLow", "equatorialGuineaHigh"],
    "ER": ["eritreaLow", "eritreaHigh"],
    "EE": ["estoniaLow", "estoniaHigh"],
    "SZ": ["eswatiniLow", "eswatiniHigh"],
    "ET": ["ethiopiaLow", "ethiopiaHigh"],
    "FO": ["faroeIslandsLow", "faroeIslandsHigh"],
    "FJ": ["fijiEastLow", "fijiEastHigh", "fijiWestLow", "fijiWestHigh"],
    "FI": ["finlandLow", "finlandHigh"],
    "FR": ["franceLow", "franceHigh", "franceDepartmentsLow", "franceDepartmentsHigh"],
    "GF": ["frenchGuianaLow", "frenchGuianaHigh"],
    "GA": ["gabonLow", "gabonHigh"],
    "GE": ["georgiaLow", "georgiaHigh", "georgiaSouthOssetiaLow", "georgiaSouthOssetiaHigh"],
    "DE": ["germanyLow", "germanyHigh"],
    "GR": ["greeceLow", "greeceHigh"],
    "GL": ["greenlandLow", "greenlandHigh"],
    "GM": ["gambiaLow", "gambiaHigh"],
    "GH": ["ghanaLow", "ghanaHigh"],
    "GN": ["guineaLow", "guineaHigh"],
    "GW": ["guineaBissauLow", "guineaBissauHigh"],
    "GY": ["guyanaLow", "guyanaHigh"],
    "HN": ["hondurasLow", "hondurasHigh"],
    "HK": ["hongKongLow", "hongKongHigh"],
    "HU": ["hungaryLow", "hungaryHigh"],
    "IS": ["icelandLow", "icelandHigh"],
    "IN": ["india2019Low", "india2019High", "indiaLow", "indiaHigh"],
    "ID": ["indonesiaLow", "indonesiaHigh"],
    "IE": ["irelandLow", "irelandHigh"],
    "IL": ["israelLow", "israelHigh", "israelPalestineLow", "israelPalestineHigh"],
    "IT": ["italyLow", "italyHigh"],
    "IQ": ["iraqLow", "iraqHigh"],
    "IR": ["iranLow", "iranHigh"],
    "JP": ["japanLow", "japanHigh"],
    "KZ": ["kazakhstanLow", "kazakhstanHigh"],
    "KE": ["kenyaLow", "kenyaHigh"],
    "XK": ["kosovoLow", "kosovoHigh"],
    "KW": ["kuwaitLow", "kuwaitHigh"],
    "KG": ["kyrgyzstanLow", "kyrgyzstanHigh"],
    "LA": ["laosLow", "laosHigh"],
    "LV": ["latviaLow", "latviaHigh"],
    "LI": ["liechtensteinLow", "liechtensteinHigh"],
    "LT": ["lithuaniaLow", "lithuaniaHigh"],
    "LU": ["luxembourgLow", "luxembourgHigh"],
    "LY": ["libyaLow", "libyaHigh"],
    "MG": ["madagascarProvinceLow", "madagascarProvinceHigh", "madagascarRegionLow", "madagascarRegionHigh"],
    "MY": ["malaysiaLow", "malaysiaHigh"],
    "MR": ["mauritaniaLow", "mauritaniaHigh"],
    "MV": ["maldivesLow", "maldivesHigh", "maldivesIslandsLow", "maldivesIslandsHigh"],
    "MW": ["malawiLow", "malawiHigh"],
    "ML": ["maliLow", "maliHigh"],
    "MT": ["maltaLow", "maltaHigh"],
    "MX": ["mexicoLow", "mexicoHigh"],
    "MD": ["moldovaLow", "moldovaHigh"],
    "MN": ["mongoliaLow", "mongoliaHigh"],
    "MA": ["moroccoLow", "moroccoHigh"],
    "ME": ["montenegroLow", "montenegroHigh"],
    "MZ": ["mozambiqueLow", "mozambiqueHigh"],
    "MK": ["northMacedoniaLow", "northMacedoniaHigh"],
    "MM": ["myanmarLow", "myanmarHigh"],
    "NA": ["namibiaLow", "namibiaHigh"],
    "NP": ["nepalLow", "nepalHigh"],
    "NL": ["netherlandsLow", "netherlandsHigh"],
    "NZ": ["newZealandLow", "newZealandHigh"],
    "NI": ["nicaraguaLow", "nicaraguaHigh"],
    "NE": ["nigerLow", "nigerHigh"],
    "NG": ["nigeriaLow", "nigeriaHigh"],
    "KP": ["northKoreaLow", "northKoreaHigh"],
    "NO": ["norwayLow", "norwayHigh"],
    "OM": ["omanLow", "omanHigh"],
    "PK": ["pakistanLow", "pakistanHigh"],
    "PS": ["palestineLow", "palestineHigh"],
    "PA": ["panamaLow", "panamaHigh"],
    "PE": ["peruLow", "peruHigh"],
    "PH": ["philippinesLow", "philippinesHigh"],
    "PL": ["polandLow", "polandHigh"],
    "PT": ["portugalLow", "portugalHigh", "portugalRegionsLow", "portugalRegionsHigh"],
    "PR": ["puertoRicoLow", "puertoRicoHigh"],
    "QA": ["qatarLow", "qatarHigh"],
    "RO": ["romaniaLow", "romaniaHigh"],
    "RU": ["russiaLow", "russiaHigh", "russiaCrimeaLow", "russiaCrimeaHigh"],
    "RW": ["rwandaLow", "rwandaHigh"],
    "SH": ["saintHelenaLow", "saintHelenaHigh"],
    "LC": ["saintLuciaLow", "saintLuciaHigh"],
    "VC": ["saintVincentLow", "saintVincentHigh"],
    "WS": ["samoaLow", "samoaHigh"],
    "SM": ["sanMarinoLow", "sanMarinoHigh"],
    "SS": ["southSudanLow", "southSudanHigh", "southSudan2015Low", "southSudan2015High"],
    "ST": ["saoTomePrincipeLow", "saoTomePrincipeHigh"],
    "SA": ["saudiArabiaLow", "saudiArabiaHigh"],
    "SN": ["senegalLow", "senegalHigh"],
    "RS": ["serbiaLow", "serbiaHigh", "serbiaNoKosovoLow", "serbiaNoKosovoHigh"],
    "SC": ["seychellesLow", "seychellesHigh"],
    "SL": ["sierraLeoneLow", "sierraLeoneHigh"],
    "SG": ["singaporeLow", "singaporeHigh"],
    "SK": ["slovakiaLow", "slovakiaHigh"],
    "SI": ["sloveniaLow", "sloveniaHigh", "sloveniaRegionsLow", "sloveniaRegionsHigh"],
    "SB": ["solomonIslandsLow", "solomonIslandsHigh"],
    "SO": ["somaliaLow", "somaliaHigh"],
    "ZA": ["southAfricaLow", "southAfricaHigh"],
    "KR": ["southKoreaLow", "southKoreaHigh"],
    "ES": ["spainLow", "spainHigh", "spainProvincesLow", "spainProvincesHigh"],
    "LK": ["sriLankaLow", "sriLankaHigh"],
    "KT": ["stKittsNevisLow", "stKittsNevisHigh"],
    "PM": ["stPierreMiquelonLow", "stPierreMiquelonHigh"],
    "SD": ["sudanLow", "sudanHigh"],
    "SR": ["surinameLow", "surinameHigh"],
    "SJ": ["svalbardLow", "svalbardHigh"],
    "SE": ["swedenLow", "swedenHigh"],
    "CH": ["switzerlandLow", "switzerlandHigh"],
    "SY": ["syriaLow", "syriaHigh"],
    "TJ": ["tajikistanLow", "tajikistanHigh"],
    "TZ": ["tanzaniaLow", "tanzaniaHigh"],
    "TH": ["thailandLow", "thailandHigh"],
    "TG": ["togoLow", "togoHigh"],
    "TN": ["tunisiaLow", "tunisiaHigh"],
    "TR": ["turkeyLow", "turkeyHigh"],
    "TM": ["turkmenistanLow", "turkmenistanHigh"],
    "AE": ["uaeLow", "uaeHigh"],
    "GB": ["ukLow", "ukHigh", "ukCountiesLow", "ukCountiesHigh", "ukCountriesLow", "ukCountriesHigh"],
    "UA": ["ukraineLow", "ukraineHigh"],
    "US": ["usaLow", "usaHigh", "usaTerritoriesLow", "usaTerritoriesHigh", "usaTerritories2Low", "usaTerritories2High"],
    "UZ": ["uzbekistanLow", "uzbekistanHigh"],
    "VA": ["vaticanLow", "vaticanHigh"],
    "VE": ["venezuelaLow", "venezuelaHigh"],
    "VN": ["vietnamLow", "vietnamHigh"],
    "YE": ["yemenLow", "yemenHigh"],
    "ZM": ["zambiaLow", "zambiaHigh"],
    "ZW": ["zimbabweLow", "zimbabweHigh"]
};
export default countries;
