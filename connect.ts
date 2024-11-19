import { exec } from 'child_process';
import { promisify } from 'util';
import path from 'path';
const execAsync = promisify(exec);
 
async function executeQuery<T>(
    connectionString: string,
    query: string,
    buffer = { maxBuffer: 1024 * 1024 * 10 }, //The size of the buffer of returned data, can be increased.
): Promise<T> {
    const executablePath = path.resolve('DotNetLibrary', 'bin', 'Release', 'net6.0', 'win-x64', 'DbConnect.exe'); //your path to CLI app
    try {
        const { stdout, stderr } = await execAsync(
            `"${executablePath}" "${connectionString}" "${query.replace(/\n/g, ' ')}"`,
            buffer,
        );
 
        if (stderr) {
            throw new Error(`Error in the program: ${stderr}`);
        }
 
        return JSON.parse(stdout);
    } catch (error) {
        throw new Error(`Execution error: ${(error as Error).message}`);
    }
}
 
const connectionString =
    'Server=xxxxxxxxxxxxxxxxxxxx.datawarehouse.fabric.microsoft.com; Authentication=Active Directory Password; Encrypt=True;Database=XXXXXXX;User Id = XXXXXX; Password = XXXXXXXXX'; // your credentials
 
async function runQuery(connectionString: string) {
    const result =  await executeQuery(
        connectionString,
        `SELECT *
            FROM your_table`,
    );
 
    console.log(result);
    return result;
}    
 
runQuery(connectionString);