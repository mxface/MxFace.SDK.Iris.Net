# MxFace Iris Software Documentation

## Interfaces

### **IDevice**

This interface provides methods to fetch information about a connected device. If no device is connected, an appropriate response is returned.

#### **Methods**

**`GetConnectedDevicesAsync()`**  
Retrieves information of connected iris devices.  
**Returns:**  
A `ConnectedDevice` object containing details of connected devices.

---

### **ICapture**

This interface manages the iris capture process using a connected and initialized device, allowing customization of the capture duration and quality requirements.

#### **Methods**

**`StartCaptureAsync(int Timeout = 10, int MinimumQuality = 60)`**  
Starts the iris capture process. This method requires the device to be initialized beforehand.  

**Parameters:**
- **`Timeout`**: Duration in milliseconds. If `0`, the process stops automatically after detecting an iris with the desired quality.
- **`MinimumQuality`**: Desired iris quality (1–100).

**Returns:**  
A task returning a `CaptureViewModel` containing:
- **`BitmapData`**: Iris image data in string format (nullable).
- **`ErrorCode`**: Error code indicating any issues during capture (nullable).
- **`ErrorDescription`**: Detailed error description (nullable).
- **`Quality`**: Quality of the captured iris (nullable).

---

### **IEnroll<TContext>**

This interface provides methods to enroll iris data into the database, requiring a `DbContext` for database operations.

#### **Methods**

**`EnrollAsync(EnrollmentRequest enroll)`**  
Enrolls iris data securely in the database.

**Parameters:**
- **`enroll`**: Contains details for enrollment, including:
  - **`IrisData`**: Iris data in string format (nullable).
  - **`ExternalId`**: Optional unique identifier (default: new GUID).

**Returns:**  
A task returning an `EnrollmentResponse` containing:
- **`Code`**: Status code for success/failure (nullable).
- **`Message`**: Success message (nullable).
- **`ErrorMessage`**: Error details (nullable).
- **`Template`**: Byte array of the enrolled iris data (nullable).

---

### **ISearch<TContext>**

This interface enables searching for enrolled iris data in the database using a `DbContext` for queries.

#### **Methods**

**`SearchAsync(SearchRequest search)`**  
Searches for enrolled iris data based on the provided details.

**Parameters:**
- **`search`**: Contains search criteria, including:
  - **`IrisData`**: Iris data in string format (nullable).

**Returns:**  
A task returning a `SearchResponse` containing:
- **`SubjectId`**: Unique identifier of the matched subject.
- **`Template`**: Iris template of the matched data.
- **`ErrorMessage`**: Error details, if the search fails (nullable).

---

### **IMatch**

This interface facilitates matching two iris datasets to determine if they are identical.

#### **Methods**

**`MatchAsync(MatchRequest match)`**  
Matches captured iris data with a provided iris dataset.

**Parameters:**
- **`match`**: Contains matching details, including:
  - **`IrisData`**: Data to be matched.
  - **`Timeout`**: Duration in milliseconds (default: stops when desired quality is detected).

**Returns:**  
A task returning a `MatchResponse` containing:
- **`BitmapData`**: Captured iris image data.
- **`ErrorCode`**: Status code for success/failure.
- **`ErrorDescription`**: Error details, if matching fails.
- **`Quality`**: Quality of the captured iris.
- **`Status`**: Boolean indicating match success.

**`VerifyAsync(VerifyRequest verify)`**  
Matches two provided iris datasets for verification.

**Parameters:**
- **`verify`**: Contains two iris datasets for verification:
  - **`IrisData1`**
  - **`IrisData2`**

**Returns:**  
A task returning a `VerifyResponse` containing:
- **`ErrorCode`**
- **`ErrorDescription`**
- **`Status`**: Boolean indicating verification success.

---

## How to Use the Provided DLL and License Files

To use the services:
1. Place the following files in your project's `bin` folder:
   - `MxFace.Iris.SDK.Core.dll`
   - `MxFace.Iris.SDK.Data.dll`
   - `MxFace.Iris.SDK.Extensions.dll`
   - `Iris.lic`
2. Your services will be accessible automatically.

---

## Code Configuration

Add the following code in your project to enable services and database setup:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    MxFaceEntityFrameworkCoreHelpers.UseMxFaceIrisService(modelBuilder);
}

builder.AddMxFaceIrisServices();
```

---

## Step-by-Step Guide

1. **Device Initialization**  
   Devices initialize automatically when connected.

2. **Capture Iris**  
   Use the *Capture 1* button to capture your iris.  
   The captured data includes **Status**, **Quality**, and **Template Data**.

3. **Retrieve Template Data**  
   Template data is generated upon capturing your iris.

4. **Enroll Iris**  
   Ensure the device is initialized and iris captured.  
   Enrolling stores the template data securely in the database.

5. **Search Iris**  
   Prerequisites: Device initialization, iris capture, and enrollment.  
   Perform a search to retrieve the **Subject ID** and **Image Data**.

6. **Capture & Match**  
   Capture a new iris and match it with a previously captured one.  
   Results: "Matched" or "Unmatched".

7. **Match Operation**  
   Ensure both irises are captured.  
   The match status will indicate "Matched" or "Unmatched".
