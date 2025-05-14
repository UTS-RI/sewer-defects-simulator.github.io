using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
public class pipe_move : MonoBehaviour
{
    public int IMU_frequency_scale = 10 ; 
    [HideInInspector] public int rate = 0; 
    [HideInInspector] public int divisor = 1;
    public int limit = 7641;
    [HideInInspector] public int remainder = 0; 
    [HideInInspector] public float moveSpeed = (float)0.1;
    public int count = 0;
    [HideInInspector] public int file_name = 0;
    // Start is called before the first frame update
    [HideInInspector] public double previous_time ;
    [HideInInspector] public Vector3 lastVelocity   ; 
    [HideInInspector] public Vector3 currentRotationalVelocity   ;
    [HideInInspector] public Vector3 lastRotationalVelocity   ;
    [HideInInspector] public Vector3 currentVelocity;
    [HideInInspector] public Vector3 acceleration ;
    [HideInInspector] public Vector3 rotationalAcceleration ;
    [HideInInspector] public double time_stamp_nano_seconds;
    public string path = @"acc_data.txt";
    [HideInInspector] public double current_time = (float)0.0;
    
    public string image_name = @"image_name.txt";
    public string camera_position = @"camera_position.txt";

    
    public bool follow_previous_path = false;
    public bool constant_move = false; 
    public bool time_name = true ;
    public bool save_images = true; 
    public bool save_trace = true;
    [HideInInspector]public string[] contents;
    [HideInInspector]public float initial_z_position;
    void record_video(string frame)
    {
        //string frame = frame_number.ToString();
        string file_name = "frame/" + frame + ".png";
        ScreenCapture.CaptureScreenshot(file_name);
    }
    void Start()
    {

        previous_time = Time.time;
        count = 0;
        if (File.Exists(path)){
            File.WriteAllText(path,String.Empty);
        }
        else{
            File.Create(path).Close();
        }


        if (File.Exists(image_name)){
            File.WriteAllText(image_name,String.Empty);
        }
        else{
            File.Create(image_name).Close();
        }



        if (File.Exists(camera_position) & save_trace ){
            File.WriteAllText(camera_position,String.Empty);
        }
        else if(save_trace){
            File.Create(camera_position).Close();
        }

        contents = File.ReadAllLines(camera_position);
        initial_z_position = transform.position.z;
    

    }
    

    // Update is called once per frame
    void Update()
    {
        rate = rate + 1 ; 
        
    
        
        if (count < limit)//7641
        {
            if (constant_move){
                transform.position = transform.position + (UnityEngine.Vector3.forward * moveSpeed);
            }
            else if (follow_previous_path){
                float z_pos = float.Parse(contents[count]);
                Debug.Log(current_time);
                //float new_position = new Vector3(0.0f, 0.0f, z_pos);
                transform.position = new Vector3(transform.position.x, transform.position.y, z_pos);

                //print(transform.position);

                    

                
                
                //for  (int i = 0; i < count; i++){
               //     itemStrings = reader.ReadLine();
                //} 
                //itemStrings = reader.ReadLine();
                //print(itemStrings);

            }
     
            currentVelocity = GetComponent<Rigidbody>().velocity;
            currentRotationalVelocity= GetComponent<Rigidbody>().angularVelocity;
            rotationalAcceleration = (currentRotationalVelocity-lastRotationalVelocity)/Time.deltaTime;
            acceleration = (currentVelocity-lastVelocity)/Time.deltaTime;
            lastVelocity =  currentVelocity;
            lastRotationalVelocity = currentRotationalVelocity;
            current_time = Time.time ; 
            time_stamp_nano_seconds = current_time * 1000000000;
            string time_stamp = Convert.ToString(  (   Math.Floor(time_stamp_nano_seconds)    )     );
            float z = acceleration.z;
            float x = acceleration.x;
            float y = acceleration.y;
            string x_str = Convert.ToString(x);
            string y_str = Convert.ToString(y);
            string z_str = Convert.ToString(z);
            float x_r = rotationalAcceleration.x;
            float y_r = rotationalAcceleration.y;
            float z_r = rotationalAcceleration.z;
            string x_r_str = Convert.ToString(x_r);
            string y_r_str = Convert.ToString(y_r);
            string z_r_str = Convert.ToString(z_r);
            //Debug.Log("Angular Acceleration: x" + x_r);
            //Debug.Log("Angular Acceleration: y" + y_r);
            //Debug.Log("Angular Acceleration: z" + z_r);
           
            if (save_trace){
                float x_position = transform.position.x;
                float y_position = transform.position.y;
                float z_position = transform.position.z;
                string z_position_str = Convert.ToString(z_position);
                string x_position_str = Convert.ToString(x_position);
                string y_position_str = Convert.ToString(y_position);
                File.AppendAllText(camera_position, x_position + "  " +  y_position + "  " + z_position_str + Environment.NewLine);

            }
            //double mid_time = (previous_time + current_time)/2;
            //double mid_time_stamp_nano_seconds = mid_time * 1000000000;
            //string mid_time_stamp = Convert.ToString((Math.Floor(mid_time_stamp_nano_seconds)) );

            //File.AppendAllText(path, mid_time_stamp + "," + x + "," + y+ "," + z + "," + x_r + ","+ y_r+','+z_r + ',' + Environment.NewLine);
            File.AppendAllText(path, time_stamp + "," + x + "," + y+ "," + z + "," + x_r + ","+ y_r+','+z_r + ','+ Environment.NewLine);
            if (rate ==IMU_frequency_scale){
            File.AppendAllText(image_name, time_stamp + Environment.NewLine );}



            

























            previous_time = current_time;
            
            
            //if (count % divisor == 0)
            //{
            string saved_name;
            if (!time_name){
                
                saved_name = Convert.ToString( count   );
                

            }
            else{
              
                saved_name = time_stamp;
        
            }
           


            
            if (save_images & rate == IMU_frequency_scale ){
                record_video(saved_name);
            }
            if (rate == IMU_frequency_scale){
                rate = 0 ; 
            }

            
            //file_name = file_name + 1;
            //* Time.deltaTime
            //}


            count++;
        }
        //90if (count == 56){ Application.Quit(); }

    }
}
