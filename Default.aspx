<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProjectSite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <!-- Wrapper for full-page background -->
    <div style="background: url('/Images/background.jpg') no-repeat center center fixed; 
                background-size: cover; 
                position: relative; 
                color: #ffffff; 
                min-height: 100vh; /* Ensures it covers the full viewport height */
              ">

        <!-- Hero Section -->
        <div style="background-color: rgba(0, 0, 0, 0.5); padding: 60px; text-align: center;">
            <img src="/Images/optimult_logo.png" alt="Optimult Logo" style="width: 250px; margin-bottom: 30px;">
            <h1 style="font-size: 3em;">Welcome to Optimult</h1>
            <p style="font-size: 1.5em; max-width: 800px; margin: auto; line-height: 1.8;">
                Your trusted partner in delivering innovative energy solutions, driving efficiency and sustainability across industries.
            </p>
        </div>

    <!-- Footprint Section -->
    <div style="padding: 60px; background-color: rgba(255, 255, 255, 0.9);">
        <h2 style="color: #4CAF50; text-align: center; font-size: 2.5em;">Our Global Footprint</h2>
        <p style="text-align: center; color: #666; max-width: 800px; margin: auto; font-size: 1.2em; line-height: 1.8;">
            Optimult has established a significant presence worldwide, delivering reliable and sustainable energy solutions across various regions. Our footprint extends to key locations, supporting our partners with cutting-edge technology and expertise.
        </p>
        <div style="text-align: center; margin-top: 30px;">
            <img src="/Images/global_footprint_map.jpg" alt="Global Footprint Map" style="width: 90%; max-width: 600px; border-radius: 15px; box-shadow: 0px 6px 12px rgba(0, 0, 0, 0.15);">
        </div>
    </div>

    <!-- What We Bring Section -->
    <div style="padding: 60px; background-color: rgba(232, 245, 233, 0.9);">
        <h2 style="color: #4CAF50; text-align: center; font-size: 2.5em;">What We Bring</h2>
        <p style="text-align: center; color: #666; max-width: 800px; margin: auto; font-size: 1.2em; line-height: 1.8;">
            Our expertise spans from strategic project planning to full-scale implementation, ensuring our partners achieve peak efficiency and sustainability.
        </p>
        <ul style="max-width: 800px; margin: 30px auto; color: #444; font-size: 1.2em; line-height: 1.8;">
            <li>Comprehensive project assessment and analysis</li>
            <li>Advanced technology integration</li>
            <li>Commitment to sustainable solutions</li>
            <li>End-to-end project support</li>
        </ul>
    </div>

    <!-- Our Capabilities Section -->
    <div style="padding: 60px; background-color: #ffffff;">
        <h2 style="color: #4CAF50; text-align: center; font-size: 2.5em;">Our Capabilities</h2>
        <p style="text-align: center; color: #666; max-width: 800px; margin: auto; font-size: 1.2em; line-height: 1.8;">
            Optimult provides top-notch solutions tailored to meet the unique needs of the energy sector. We specialize in:
        </p>
        <div style="display: flex; justify-content: center; gap: 40px; flex-wrap: wrap; margin-top: 40px;">
            <div style="width: 180px; text-align: center;">
                <img src="/Images/capability1.jpg" alt="Renewable Energy Solutions" style="width: 100px; margin-bottom: 15px; border-radius: 50%; box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);">
                <p style="color: #333; font-size: 1.2em;">Renewable Energy Solutions</p>
            </div>
            <div style="width: 180px; text-align: center;">
                <img src="/Images/capability2.jpg" alt="Smart Grid Technology" style="width: 100px; margin-bottom: 15px; border-radius: 50%; box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);">
                <p style="color: #333; font-size: 1.2em;">Smart Grid Technology</p>
            </div>
            <div style="width: 180px; text-align: center;">
                <img src="/Images/capability3.jpg" alt="Energy Management Systems" style="width: 100px; margin-bottom: 15px; border-radius: 50%; box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);">
                <p style="color: #333; font-size: 1.2em;">Energy Management Systems</p>
            </div>
        </div>
    </div>

    <!-- Featured Projects Section -->
    <div style="padding: 60px; background-color: #f5f5f5;">
        <h2 style="text-align: center; color: #4CAF50; font-size: 2.5em;">Renewable Energy Projects</h2>

        <!-- Bootstrap Carousel -->
        <div id="projectCarousel" class="carousel slide" data-bs-ride="carousel">
            <div class="carousel-inner">
                <!-- Project 1 -->
                <div class="carousel-item active">
                    <div style="display: flex; align-items: center;">
                        <div style="flex: 1; padding: 30px;">
                            <p style="font-size: 1.3em;">
                                <span style="color: #4CAF50;">● INNOVATE </span>
                                <span style="color: #888;">● DESIGN </span>
                                <span style="color: #888;">● IMPLEMENT</span>
                            </p>
                            <h4 style="color: #333; font-size: 1.8em;">Brayfoil Technology - Wind Turbine Design</h4>
                            <p style="font-size: 1.1em;">Brayfoil is an innovator company with patents in 44 countries for an auto-setting and auto-morphing wing, that has multiple applications. Optimult provides the engineering support service to this cutting edge technology by means of multi-discipline in engineering design, with the ultimate goal of commercializing the Brayfoil Turbine.</p>
                        </div>
                        <div style="flex: 1; text-align: center;">
                            <img src="/Images/brayfoil_turbine.jpg" alt="Brayfoil Wind Turbine" style="width: 80%; border-radius: 10px;">
                        </div>
                    </div>
                </div>

                <!-- Project 2 -->
                <div class="carousel-item">
                    <div style="display: flex; align-items: center;">
                        <div style="flex: 1; padding: 30px;">
                            <p style="font-size: 1.3em;">
                                <span style="color: #4CAF50;">● INNOVATE </span>
                                <span style="color: #888;">● DESIGN </span>
                                <span style="color: #888;">● IMPLEMENT</span>
                            </p>
                            <h4 style="color: #333; font-size: 1.8em;">RENCAT - Pilot Plant Implementation Study</h4>
                            <p style="font-size: 1.1em;">Rencat is an innovator company from Denmark, and Optimult, under the commission from Danish Embassy, undertook a feasibility study for the implementation of the Ammonia based generator for up to 5 kW as a backup solution to the telecommunication cell phone towers.</p>
                        </div>
                        <div style="flex: 1; text-align: center;">
                            <img src="/Images/rencat_plant.jpg" alt="RENCAT Pilot Plant" style="width: 80%; border-radius: 10px;">
                        </div>
                    </div>
                </div>

                <!-- Project 3 -->
                <div class="carousel-item">
                    <div style="display: flex; align-items: center;">
                        <div style="flex: 1; padding: 30px;">
                            <p style="font-size: 1.3em;">
                                <span style="color: #4CAF50;">● INNOVATE </span>
                                <span style="color: #888;">● DESIGN </span>
                                <span style="color: #888;">● IMPLEMENT</span>
                            </p>
                            <h4 style="color: #333; font-size: 1.8em;">GreenStone Energy - Master EPC Supplier</h4>
                            <p style="font-size: 1.1em;">Greenstone Energy is an Independent Power Utility (IPU) provider with deep expertise in renewable energy systems. Optimult is working with Greenstone Energy to manage the EPC on a large solar project with the objective of supporting the IPU in ensuring the supply of green energy.</p>
                        </div>
                        <div style="flex: 1; text-align: center;">
                            <img src="/Images/greenstone_energy.jpg" alt="GreenStone Energy Project" style="width: 80%; border-radius: 10px;">
                        </div>
                    </div>
                </div>
         

            
                <!-- Project 4 -->
                <div class="carousel-item">
                    <div style="display: flex; align-items: center;">
                        <div style="flex: 1; padding: 30px;">
                            <p style="font-size: 1.3em;">
                                <span style="color: #4CAF50;">● INNOVATE </span>
                                <span style="color: #888;">● DESIGN </span>
                                <span style="color: #888;">● IMPLEMENT</span>
                            </p>
                            <h4 style="color: #333; font-size: 1.8em;">Deloitte South Africa - Review of Matjhabeng Solar Plan</h4>
                            <p style="font-size: 1.1em;">Deloitte is a multinational advisory company which provides Audit, Consulting and Financial Advisory globally. Optimult being their preferred technical partner for Advisory projects, provided the technical review of Matjhabeng Solar Project for a 300MW plant in Matjhabeng Municipal area near the town of Welkom.</p>
                        </div>
                        <div style="flex: 1; text-align: center;">
                            <img src="/Images/deloitte_solar.jpg" alt="RENCAT Pilot Plant" style="width: 80%; border-radius: 10px;">
                        </div>
                    </div>
                </div>
                   </div>
            <button class="carousel-control-prev" type="button" data-bs-target="#projectCarousel" data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Previous</span>
            </button>
            <button class="carousel-control-next" type="button" data-bs-target="#projectCarousel" data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Next</span>
            </button>
        </div>
    </div>
</asp:Content>
