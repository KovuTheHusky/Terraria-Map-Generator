			///////////////////////////////////////////////////////////////////
			///
			/// The icon set
			///
			/// You can customize your own icon set here
			///
			///////////////////////////////////////////////////////////////////
			
			var MapIcons = 
			{ 
				Spawn : 'http://maps.gstatic.com/mapfiles/ms2/micons/blue.png',
				Chest: 'http://maps.gstatic.com/mapfiles/ms2/micons/green.png',
				HeartContainer: 'http://maps.gstatic.com/mapfiles/ms2/micons/red.png',
				Anvil	: 'http://maps.gstatic.com/mapfiles/ms2/micons/green.png',
				Forge	: 'http://maps.gstatic.com/mapfiles/ms2/micons/yellow.png',
				Workbench	: 'http://maps.gstatic.com/mapfiles/ms2/micons/green.png',
				Alter	: 'http://maps.gstatic.com/mapfiles/ms2/micons/green.png',
				ShadowOrb	: 'http://maps.gstatic.com/mapfiles/ms2/micons/purple.png',
				HellForge	: 'http://maps.gstatic.com/mapfiles/ms2/micons/green.png',
			}

			///////////////////////////////////////////////////////////////////
			///
			/// The base path of the map tiles.
			///
			/// You need to update this value in order for this web page to
			/// work!
			///
			///////////////////////////////////////////////////////////////////
			var basePath = "/";
			var inited = false;
			var origin = translateGameXYToLatLng(0, 600);
			var infoWindows = new Array();
			var infoWindow;
			function TerrariaMapType() { 
			} 
 
			TerrariaMapType.prototype.tileSize = new google.maps.Size(256,256); 
			TerrariaMapType.prototype.name = "Terraria"; 
			TerrariaMapType.prototype.minZoom = 11; 
			TerrariaMapType.prototype.maxZoom = 17; 
 
			TerrariaMapType.prototype.getTile = function(coord, zoom, ownerDocument) {
				var zoomLevel;

				switch (zoom) {
					case 11:
						zoomLevel = 0.03125;
						break;
					case 12:
						zoomLevel = 0.0625;
						break;
					case 13:
						zoomLevel = 0.125;
						break;
					case 14:
						zoomLevel = 0.25;
						break;
					case 15:
						zoomLevel = 0.5;
						break;
					case 16:
						zoomLevel = 1;
						break;
					case 17:
						zoomLevel = 2;
						break;
				}
				offset = Math.pow(2, (zoom - TerrariaMapType.prototype.minZoom)) *1024;
				var x = (coord.x - offset).toString();
				var y = (coord.y - offset).toString();
				var div = ownerDocument.createElement('DIV'); 
				div.style.background = "black url(" + basePath + "Zoom%20" + zoomLevel.toString() + "/Layer%200/" + x.toString() + "/" + y.toString() + ".png)";

				div.style.color = "white";
				div.style.width = this.tileSize.width + 'px'; 
				div.style.height = this.tileSize.height + 'px'; 
				div.style.fontSize = '10';   
				return div; 
			}; 
 
			TerrariaMapType.prototype.name = "Terraria"; 
			TerrariaMapType.prototype.alt = "Tile Coordinate Map Type"; 
 
			var map;
			var terrariaMapType = new TerrariaMapType();
 
			function initialize() {
				var mapOptions = { 
					zoom: 6, 
					center: origin, 
					mapTypeControlOptions: { 
						mapTypeIds: ['Terraria'], 
						style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR 
					},
                    streetViewControl: false
				}; 

				map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions); 
				map.mapTypes.set('terraria', terrariaMapType); 
				map.setMapTypeId('terraria'); 

				// Wait for idle map
				google.maps.event.addListener(map, 'idle', function() {
				   // Get projection
				   if(inited == false){
						addMarkersFromXML();
						inited = true;
					}
				});
			}
			
			function translateGameXYToLatLng(x, y)
			{
				 return new google.maps.LatLng(y * -0.00034345869 , x * 0.00034335869  );
			}
			
			function AddGameMarker(type,x,y,itemList,countList)
			{
				var marker = new google.maps.Marker({
					  position: translateGameXYToLatLng(x,y), 
					  map: map,
					  icon : MapIcons[type],
					  title: type
				  });
				 
				 if(type=="Chest"){
					var content = '<div id="info">';
					for (var i = 0; i< itemList.length; i++) {
						content +=  itemList[i] + " (" + countList[i] +  ")<br />";
					}
					content += '</div>';
					var index = infoWindows.length;
					infoWindows[index] = content
					
					google.maps.event.addListener(marker, 'click', function() {
					if (!infoWindow) {
						infoWindow = new google.maps.InfoWindow();
					}
						infoWindow.setContent(infoWindows[index]);
						infoWindow.open(map, marker);
					});
					}
			}
			
			function addSpecificMarker(xmlContainer,objTag)
			{
				var objs = xmlContainer.documentElement.getElementsByTagName(objTag);
				for (var i = 0; i < objs.length; i++) 
				{
					var s = objs[i].getAttribute("Location").split(',',2);
					AddGameMarker(objTag,parseFloat(s[0]),parseFloat(s[1]),null,null);
				}
			}
			
			function addMarkersFromXML()
			{
				downloadUrl("MapData.xml", function(data) 
				{
					var spawn = data.documentElement;
					AddGameMarker("Spawn",
						parseFloat(spawn.getAttribute("Spawn_X")),
						parseFloat(spawn.getAttribute("Spawn_Y")),
						null,null);
				
					  var chests = data.documentElement.getElementsByTagName("Chest");
					  for (var i = 0; i < chests.length; i++) 
					  {
							itemList = new Array();
							countList = new Array();

							var items = chests[i].getElementsByTagName("Item");
							for (var j = 0; j < items.length; j++) {
								itemList.push(items[j].getAttribute("Name"));
								countList.push(items[j].getAttribute("Count"));
							}
							var s = chests[i].getAttribute("Location").split(',',2);

							AddGameMarker("Chest",parseFloat(s[0]),parseFloat(s[1]),itemList,countList);
							itemList = [];
							countList = [];
					  }
					  addSpecificMarker(data,"HeartContainer");
					  addSpecificMarker(data,"Anvil");
					  addSpecificMarker(data,"Forge");
					  addSpecificMarker(data,"Workbench");
					  addSpecificMarker(data,"Alter");
					  addSpecificMarker(data,"HellForge");
					  addSpecificMarker(data,"ShadowOrb");

				});
			 }