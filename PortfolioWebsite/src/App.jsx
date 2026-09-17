import ThemeProvider from "./context/ThemeProvider";
import Navbar from "./components/Navbar";
import ScrollDots from "./components/ScrollDots";
import Hero from "./components/Hero";
import ProjectsSection from "./components/ProjectsSection";
import SkillsExperienceSection from "./components/SkillsExperienceSection";
import RecommendationsSection from "./components/RecommendationsSection";
import ContactSection from "./components/ContactSection";
import Footer from "./components/Footer";
import "./App.css";
const App = () => {
  return (
    <>
      <ThemeProvider>
        <div className="bg-white dark:bg-gray-900 text-gray-900 dark:text-white">
          asd sadpdwpq.
          <Navbar />
          <ScrollDots />
          <div id="hero">
            <Hero />
          </div>
          <div id="projects">
            <ProjectsSection />
          </div>
          <div id="skills">
            <SkillsExperienceSection />
          </div>
          {/* <div id="recommendations">
            <RecommendationsSection />
          </div>
          <div id="contact">
            <ContactSection />
          </div> */}
          <Footer />
        </div>
      </ThemeProvider>
    </>
  );
};

export default App;
