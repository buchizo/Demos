use std::env;
use std::path::PathBuf;
use hyperlight_wasm::{ParameterValue, Result, ReturnType, ReturnValue, SandboxBuilder};

fn main() -> Result<()> {
    let mut sandbox = SandboxBuilder::new()
        .with_sandbox_running_in_process()
        .build()?;

    let wasm_sandbox = sandbox.load_runtime()?;

    let mod_path = PathBuf::from(env::current_dir()?)
        .join("HelloWorld.wasm")
        .as_os_str()
        .to_string_lossy()
        .to_string();
    // Load a Wasm module into the sandbox
    let mut loaded_wasm_sandbox = wasm_sandbox.load_module(mod_path)?;

    // Call a function in the Wasm module
    let ReturnValue::Int(result) = loaded_wasm_sandbox.call_guest_function(
        "HelloWorld",
        Some(vec![ParameterValue::String(
                "Message from Rust Example to Wasm Function".to_string(),
            )]),
        ReturnType::Int,
    )?
    else {
        panic!("Failed to get result from call_guest_function to HelloWorld Function")
    };

    Ok(())
}
