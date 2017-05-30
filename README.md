# Artificial Intelligent Smart Assistant (AISA)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Nowadays e-Learning is just a simple concept that is available everywhere in the world, but for those students who can barely turn on a computer or for the students who cannot type very well (which is a necessary skill for almost every task in the e-learning context), AISA  (<em>/ʌizə/</em>) - The Artificial Intelligent Smart Assistant helps them with it's special ability of talking with the students.

### FOR THE SAKE OF HUMANITY

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Artificial Intelligence (AI) is an advanced technology in the field of computer programming. It is affected to the human's lifestyle in both positive and negative ways. Negatively AI is used in drones which are a major threat to the privacy of humans, missiles which are coded in order to hit a target is also a major threat to the security of a certain country. But there are more effective and productive ways we can use Artificial Intelligence in our lives such as this project itself. AISA is a usage of Artificial Intelligence, Smart Voice and Speech Recognition Engine that can be used by a students in many aspects. But the "Student" that we talk about here is not just an average student, <strong>A Handicap Student</strong> is a student who is disabled by any means (except the student cannot talk)

### AISA'S ROLE AS A STUDENT ASSISTANT

1. <strong>Find new classes powered by Project Scholar</strong>
2. <strong>Write MCQ Papers, Answer questions using voice</strong>
3. <strong>Remind homework of a certain class</strong>
4. <strong>Find Books online (Based on specific field or ISBN)</strong>
5. <strong>Notify student when a teacher posts a new Paper, Note, Tute, or an Assignment in a class</strong>
6. <strong>Provide Facts related to General Knowledge</strong>
7. <strong>Provide news that is related to subject matter</strong>
8. <strong>Finds opportunities such as Scholarships and Competitions</strong>
9. <strong>Schedule classes and notify the student</strong>
<em>And etc.</em>

### AISA'S ROLE AS A TYPICAL ASSISTANT

1. A Greeting System
2. Basic Question Handling for weather, time and etc.
3. Speech Rejection Handling
4. Speech Hypothesizing
5. Having an eye-catching User Interface, User Experience, Sound Effects, and Animations

### GETTING STARTED WITH PROJECT AISA

This section of the README.md file will guide you to contribute to AISA. Follow the steps mentioned and you'll be a contributing Project AISA in no time,

<strong>Downloading and Installation of Components</strong>
<br>
1. First of all fork this project to your account on GitHub.
2. Clone that project to your work computer using Git.
3. Open AISA/AISA.sln which will require you to open two or three projects including Project AISA itself and the Application Programming Interface for AISA (AISA-API) which is a client for the <a href="http://www.github.com/Chamuth/AISA-API">AISA-API GitHub Repository</a> and Client for the Project Scholar which is available <a href="http://www.github.com/Chamuth/scholar-csharp-sdk">here</a>.
4. At AISA.sln / AISA.csproj you will be needed some References to some libraries, namely RestSharp, Newtonsoft.Json, Scholar, and AISA-API. Here Scholar and AISA-API dll files can be obtained by the projects that you have downloaded from the step 3. Using NuGet the official package manager for .NET Framework you can download RestSharp and Newtonsoft Json, matter of fact while building the project AISA.csproj for the first time it will download all the dependencies (libraries) the project need.

<strong>Setting up API endpoints</strong>
<br>
1. Inside AISA/App.xaml.cs, you'll find a method called <strong>InitializeEndpoints</strong>, it defines all of the API endpoints required by the application excluding the OpenWeather API and the ip-api API.