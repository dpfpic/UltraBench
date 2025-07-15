# UltraBench by Dpfpic

## Project Description

UltraBench is a lightweight and user-friendly system benchmarking application designed to quickly assess the performance of key components in your computer: the Central Processing Unit (CPU), Random Access Memory (RAM), storage (SSD/HDD), and Graphics Processing Unit (GPU). Developed by Dpfpic, UltraBench aims to provide clear and visual scores to help users understand their machine's capabilities.

## Features

* **Detailed Performance Tests:**
    * **CPU (Central Processing Unit):** Evaluation of raw processing power.
    * **RAM (Random Access Memory):** Measurement of memory read/write speeds.
    * **SSD/HDD (Storage):** Tests for read and write speeds of your primary drive.
    * **GPU (Graphics Processing Unit):** Integration with an external GPU benchmarking tool (such as PassMark PerformanceTest) to obtain a comprehensive graphics score.
* **Intuitive Result Display:** Benchmark scores are displayed directly in the interface, with a color-coded system for quick interpretation:
    * **Green:** Good performance / Excellent score
    * **Orange:** Average performance / Decent score
    * **Red:** Low performance / Score to improve
    * **Gray:** Test not executed or failed
* **Automatic Tool Location:** Ability to detect and launch external tools like HWMonitor for real-time system monitoring, and PassMark PerformanceTest for GPU benchmarking.
* **Report Management:** Easy access to benchmark-generated reports.
* **Result History:** Test results are stored and can be reviewed for performance tracking.

## Installation and Usage

### Prerequisites

* Windows 10 or later (for .NET Framework compatibility and external tool functionality).
* .NET Framework 4.7.2 or later.
* **For GPU Benchmark:** A functional installation of PassMark PerformanceTest (64-bit version recommended) is required, as UltraBench relies on this tool for the GPU test. UltraBench will attempt to locate it automatically.
* **For System Monitoring:** HWMonitor (64-bit version recommended) is advised for real-time hardware monitoring, though its installation is not strictly mandatory for running benchmarks. UltraBench will attempt to locate it automatically.

### How to Get UltraBench

1.  **Clone the repository (for developers):**
    `git clone https://github.com/dpfpic/UltraBench/`
2.  **Download compiled version (for users):**
    Download the latest release from the "Releases" section of your GitHub/GitLab repository (if you create one).

### How to Run the Application

1.  Unzip the downloaded file (if you downloaded a compiled version).
2.  Execute `UltraBench.exe`.

### How to Use

1.  **Individual Tests:** Click the "Test CPU", "Test RAM", "Test SSD", "Test GPU" buttons to run specific benchmarks.
2.  **Full Test:** Click "Full Stress Test" (or equivalent if implemented) to run a complete suite of benchmarks.
3.  **System Monitoring:** Click "Launch HWMonitor" to open HWMonitor (if installed and detected).
4.  **Reports:** Click "Open Reports" to access the folders where benchmark reports are saved.
5.  **Exit:** Click "Close" to close the application.

## Technologies Used

* **Language:** C#
* **Framework:** .NET Framework (Windows Forms)
* **Integrated External Tools:**
    * PassMark PerformanceTest (for GPU benchmark)
    * HWMonitor (for hardware monitoring)
* **Detection Methods:** Searches in Windows Registry and default program paths.

## Contributing

Contributions to UltraBench are welcome! If you wish to improve the application, here are some areas:
* Add new integrated benchmarks (without external dependencies).
* Improve the user interface.
* Optimize benchmark algorithms.
* Add features for saving/sharing results.

Please fork the repository, create a branch for your changes, and submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENCE) file for more details.

---