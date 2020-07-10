# Invoicer

Description:
- Simple C# Windows Form Application generated in Microsoft Visual Studio 2015. 

Function:
- Initial setup (file locations, etc.)
- Directory setup check every runtime
- Basic logging functionality

Process:
0. Backup previously generated files.
1. Reads a .docx joblist file
2. Fills relevant information into .dotx template
3. Saves as .docx file and .pdf(optional) in Outgoing folder
4. (Open)s Outgoing folder and PDFs for review and emailing
5. (Transfer)s files from Outgoing to relevant locations

Program directory:
- Invoicer
- - Config 
- - - config.xml 
- - - invoiceTemplate.dotx 
- - - log.txt 
- - Invoices
- - - Docs
- - - PDFs
- - Outgoing
- - Invoicer.exe
- JobList.docx ~anywhere

Known Issues:
- Error once joblist holds too many items (~200 Invoices)
