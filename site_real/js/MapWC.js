class Map extends HTMLElement{
    constructor(){
        super();
        this.attachShadow({mode:'open'});
        this.div=document.createElement("div");
    }
    connectedCallback(){
        var MapProperties={
            center:new google.maps.LatLng(0,0),
            zoom:1
        };
        this.map=new google.maps.Map(this.div,MapProperties);
        google.maps.event.addListener(this.map,'click',(e)=>{
            var LatLng=e.latLng;
            GetGeocode(LatLng.lat,LatLng.lng)
            .then(code=>getCountryData(code))
            .then(this.showCountry);
        });
        this.shadowRoot.appendChild(this.div);
    }
    /**
     * 
     * @param {Country} country 
     */
    showCountry(country){
        
    }

}
window.customElements.define("g-map",Map);